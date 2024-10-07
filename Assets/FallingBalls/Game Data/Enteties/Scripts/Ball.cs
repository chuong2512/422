using UnityEngine;

public class Ball : MonoBehaviour
{
    public ColorSet activeColors;
    public Color inactiveColor;
    public bool isPreactive;

    Material mat;
    Rigidbody2D myRB;

    bool isActive = false;
    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;

            if (isActive)
            {
                mat.color = activeColors.GetColor;
                CameraController.Instance.AddBall(transform);
                myRB.isKinematic = false;
            }
            else
            {
                mat.color = inactiveColor;
                myRB.isKinematic = true;
            }
        }
    }

    private void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
        mat = GetComponent<MeshRenderer>().material;
        if (BallAssets.Instance != null)
            GetComponent<MeshFilter>().mesh = BallAssets.Instance.ballMesh;
        IsActive = isPreactive;

        transform.localScale = Vector3.one * Random.Range(0.1f, 0.2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Ball otherBall = collision.gameObject.GetComponent<Ball>();

            if (otherBall != null)
                if (otherBall.IsActive)
                {
                    if (!isActive)
                    {
                        IsActive = true;
                        CameraController.Instance.AddBall(transform);
                    }
                }
        }

        if (collision.relativeVelocity.magnitude > 8)
        {
            SoundManager.Instance.Hit(collision.relativeVelocity.magnitude);
        }
    }

    [System.Serializable]
    public class ColorSet
    {
        public Color[] colors;

        public Color GetColor
        {
            get
            {
                if (colors != null)
                    return colors[Random.Range(0, colors.Length)];
                else
                    return Color.black;
            }
        }
    }

    public void PrepareToDestroy()
    {
        CameraController.Instance.RemoveBall(transform);
        myRB.isKinematic = false;
    }
}
