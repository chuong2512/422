using UnityEngine;

public class Finish : MonoBehaviour
{
    public float waitTimer = 2f;
    public Animator boxAnim;
    public TextMesh textMesh;
    public ParticleSystem confetti;

    int currentAmount = 0;
    float currentPercent;
    float goalPercent;
    float timer;
    bool isComplete = false;
    bool isActive = true;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        textMesh.text = GameManager.Instance.GetGoal.ToString();
    }

    private void Update()
    {
        goalPercent = (float)currentAmount / GameManager.Instance.GetGoal;
        currentPercent = Mathf.Lerp(currentPercent, goalPercent, 0.1f);
        anim.SetFloat("CompleteLevel", currentPercent);

        if (currentAmount > 0 && isActive)
        {
            timer += Time.deltaTime;

            if (timer > waitTimer)
            {
                if (isComplete)
                {
                    boxAnim.SetTrigger("Closed");

                    GameManager.Instance.PlayerWon(currentAmount);
                    if (SoundManager.Instance != null)
                    {
                        SoundManager.Instance.Win();
                    }
                    confetti.Play();
                }

                isActive = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            currentAmount++;
            timer = 0;

            textMesh.text = (GameManager.Instance.GetGoal - currentAmount).ToString();

            if (currentAmount >= GameManager.Instance.GetGoal)
            {
                isComplete = true;
                textMesh.gameObject.SetActive(false);
            }

            FindObjectOfType<CameraController>().Finish(transform.position);
            boxAnim.SetTrigger("Ball In");

            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.Ball();
            }
        }
    }
}
