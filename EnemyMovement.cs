using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement
{
    public static void Move(Vector2Int target, EnemyBrain brain)
    {
        if (MapManager.map[target.x, target.y].isWalkable && !MapManager.map[target.x, target.y].hasPlayer && !MapManager.map[target.x, target.y].hasEnemy)
        {
            MapManager.map[brain.position.x, brain.position.y].hasEnemy = false;
            MapManager.map[brain.position.x, brain.position.y].enemyID = -1;
            MapManager.map[brain.position.x, brain.position.y].enemyObject = null;

            brain.position = target;

            MapManager.map[brain.position.x, brain.position.y].hasEnemy = true;
            MapManager.map[brain.position.x, brain.position.y].enemyID = brain.id;
            MapManager.map[brain.position.x, brain.position.y].enemyObject = brain.gameObject;

            DungeonGenerator dungeon = GameObject.Find("GameManager").GetComponent<DungeonGenerator>();
            brain.gameObject.transform.position = new Vector3(brain.position.x * dungeon.tileScaling, brain.position.y * dungeon.tileScaling, brain.gameObject.transform.position.z);
        }

        if (MapManager.map[target.x, target.y].hasPlayer)
        {
            Attack(brain);
        }

    }

    static void Attack(EnemyBrain brain)
    {
        PlayerHealth player = GameObject.Find("Player(Clone)").GetComponent<PlayerHealth>();

        int roll = DiceRoller.RollDice(1, 20);

        if((roll + MapManager.enemies[brain.id].baseAttack) > PlayerManager.stats.ac)
        {
            int damage = DiceRoller.RollDice(MapManager.enemies[brain.id].diceDamage, MapManager.enemies[brain.id].sidesDamage);

            damage += MapManager.enemies[brain.id].bonusDamage;

            Debug.Log("Player got hit on " + damage + " hp!");

            player.DealDamage(damage);
        }
    }
}