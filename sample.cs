
//Function is called each frame
private void Update()
{
    //check if all units are ready, and the battle over state has not been triggered
    if (unitsReady && !unitSynchronizer.BattleIsOver())
    {
        //refresh list of allies and enemies
        if (gameObject.tag == "attacker")
        {
            allyUnits = GameObject.FindGameObjectsWithTag("attacker").Where(obj => obj != gameObject).ToArray();
            enemyUnits = GameObject.FindGameObjectsWithTag("defender");
        }
        else
        {
            allyUnits = GameObject.FindGameObjectsWithTag("defender").Where(obj => obj != gameObject).ToArray();
            enemyUnits = GameObject.FindGameObjectsWithTag("attacker");
        }

        //check if unit has not yet reached movement destination, if so, keep moving
        if (transform.position != currentMoveTarget && movingToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentMoveTarget, movementSpeed * Time.deltaTime);
        }
        else
        {
            if (currentFrame == 30 - unitData.Speed)
            {
                if (targetUnit == null)
                {
                // If there is no target unit, find the closest enemy unit and set it as the target
                targetUnit = FindNearestEnemyUnit();
                }
                else
                {
                    // If within attacking range, attack the target unit
                    if (IsInRange())
                    {
                        AttackTarget();
                    }
                    else
                    {
                        MoveTowardsTarget(); 
                    }
                }
                currentFrame = 0;
            }
            else
            {
                currentFrame++;
            }
        }
    }

    // Check if the synchronizer is ready before executing
    if (!unitsReady && unitSynchronizer.IsReady())
    {
        unitsReady = true;
    }
}