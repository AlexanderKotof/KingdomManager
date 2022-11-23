namespace KM.Features.BattleFeature.BattleSystem3d
{
    public class BattleCharacterUnit : Unit
    {
        public float damage = 10;
        public float attackDistance = 1;

        public bool canMove = true;
        public bool canAttack = true;




        private void Update()
        {
            if (canMove)
            {
                // move
            }
        }

    }
}
