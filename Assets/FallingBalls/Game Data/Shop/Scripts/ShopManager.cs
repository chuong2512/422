using UnityEngine;
using System.Collections.Generic;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] BallDATA[] skins;
    [SerializeField] GameObject ballHolderPrefab;
    [SerializeField] Transform contentTransform;

    List<BallHolder> ballHolders = new List<BallHolder>();

    protected override void Awake()
    {
        base.Awake();

        GenerateHolders();
        SetSelectedBall();
    }

    void GenerateHolders()
    {
        for (int i = 0; i < skins.Length; i++)
        {
            GameObject newHolder = Instantiate(ballHolderPrefab, contentTransform);
            newHolder.GetComponent<BallHolder>().Set(skins[i]);

            ballHolders.Add(newHolder.GetComponent<BallHolder>());
        }
    }

    void SetSelectedBall()
    {
        string selectedName = PlayerPrefs.GetString("Selected", "default");
        foreach (BallHolder holder in ballHolders)
        {
            if (holder.data.name == selectedName)
            {
                holder.Selected(true);
                SelectBall(holder.data);
            }
            else
                holder.Selected(false);
        }
    }

    public void SelectBall(BallDATA data)
    {
        PlayerPrefs.SetString("Selected", data.name);

        foreach (BallHolder holder in ballHolders)
        {
            if (holder.data == data)
                holder.Selected(true);
            else
                holder.Selected(false);
        }

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<MeshFilter>().mesh = data.mesh;
        }
    }
}
