using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public int id;
    public Vector2Int position;

    EnemyVisibility visibility;

    bool isAsleep;
    int chance = 30;

    public void Initialize(int newID, Vector2Int newPosition)
    {
        id = newID;
        position = newPosition;

        visibility = GetComponent<EnemyVisibility>();
        visibility.Initialize();
    }

    public void DoTurn()
    {
        if (isAsleep)
        {
            Vector2Int player;
            if (EnemyPerception.IsPlayerVisible(position, out player))
            {
                int roll = Random.Range(0, 100);
                if (roll <= chance)
                {
                    isAsleep = false;
                }
            }
        }
        else
        {
            MeleeAI.MakeDecision(this);
        }
    }
}