using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2Int position;

    GameManager manager;
    DungeonGenerator dungeon;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        dungeon = GameObject.Find("GameManager").GetComponent<DungeonGenerator>();
    }

    private void Update()
    {
        if (manager.isPlayerTurn)
        {
            if (Input.GetKeyUp("up") || Input.GetKeyUp(("w")) || Input.GetKey(KeyCode.Keypad8))
            {
                Move(InputToVector(0, 1));
            }
            if (Input.GetKeyUp("down") || Input.GetKeyUp("s") || Input.GetKey(KeyCode.Keypad2))
            {
                Move(InputToVector(0, -1));
            }
            if (Input.GetKeyUp("left") || Input.GetKeyUp("a") || Input.GetKey(KeyCode.Keypad4))
            {
                Move(InputToVector(-1, 0));
            }
            if (Input.GetKeyUp("right") || Input.GetKeyUp("d") || Input.GetKey(KeyCode.Keypad6))
            {
                Move(InputToVector(1, 0));
            }
            if (Input.GetKey(KeyCode.Keypad7) || Input.GetKeyUp("q"))
            {
                Move(InputToVector(-1, 1));
            }
            if (Input.GetKey(KeyCode.Keypad9) || Input.GetKeyUp("e"))
            {
                Move(InputToVector(1, 1));
            }
            if (Input.GetKey(KeyCode.Keypad1) || Input.GetKeyUp("z"))
            {
                Move(InputToVector(-1, -1));
            }
            if (Input.GetKey(KeyCode.Keypad3) || Input.GetKeyUp("c"))
            {
                Move(InputToVector(1, -1));
            }
        }
    }

    Vector2Int InputToVector(int x, int y)
    {
        Vector2Int target = new Vector2Int(position.x + x, position.y + y);

        return target;
    }

    void Move(Vector2Int target)
    {
        if (MapManager.map[target.x, target.y].isWalkable && !MapManager.map[target.x, target.y].hasEnemy)
        {
            MapManager.map[position.x, position.y].hasPlayer = false;
            //MapManager.map[position.x, position.y].secondChar = "";
            position = target;
            MapManager.map[position.x, position.y].hasPlayer = true;
            //MapManager.map[position.x, position.y].secondChar = "@";

            transform.position = new Vector3(position.x * dungeon.tileScaling, position.y * dungeon.tileScaling, transform.position.z);
        }

        if (MapManager.map[target.x, target.y].hasEnemy)
        {
            for (int i = 0; i > MapManager.enemies.Count - 1; i++)
            {
                if(MapManager.enemies[i].position == target)
                {
                    Attack(MapManager.enemies[i].brain);

                    Debug.Log("Attacking enemy!");
                }
            }
        }

        manager.FinishPlayersTurn();
    }

    void Attack(EnemyBrain enemy)
    {
        int roll = DiceRoller.RollDice(1, 20);

        if ((roll + PlayerManager.stats.attackBonus) > MapManager.enemies[enemy.id].ac)
        {
            int damage = DiceRoller.RollDice(PlayerManager.stats.dicesDamage, PlayerManager.stats.sidesDamage);

            damage += PlayerManager.stats.bonusDamage;

            Debug.Log("Enemy got hit on " + damage + " hp!");

            enemy.gameObject.GetComponent<EnemyHealth>().DealDamage(damage);
        }
    }

}