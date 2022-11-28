using System.Collections.Generic;

    public class ProductionQueue
    {
        public class QueueItem
        {
            public BattleUnitEntity unit;
            public int count;

            public QueueItem(BattleUnitEntity unit)
            {
                this.unit = unit;
                count = 1;
            }

        }

        public List<QueueItem> queue = new List<QueueItem>();

        public int QueueLenght => queue.Count;
        public void AddUnit(BattleUnitEntity unit)
        {
            var index = GetIndex(unit);
            if (index >= 0)
            {
                queue[index].count++;
            }
            else
                queue.Add(new QueueItem(unit));
        }

        public QueueItem GetFirst()
        {
            return queue.Count > 0 ? queue[0] : null;
        }

        public void RemoveUnit(BattleUnitEntity unit)
        {
            var index = GetIndex(unit);
            if (index >= 0)
            {
                queue[index].count--;

                if (queue[index].count == 0)
                    queue.RemoveAt(index);
            }
        }

        public void RemoveOneFromFirst()
        {
            queue[0].count--;

            if (queue[0].count == 0)
                queue.RemoveAt(0);
        }

        public void RemoveFirst()
        {
            if( queue.Count > 0 )
                queue.RemoveAt(0);
        }

        public int GetIndex(BattleUnitEntity unit)
        {
            for (int i = 0; i < queue.Count; i++)
            {
                QueueItem item = queue[i];
                if (item.unit == unit)
                    return i;
            }
            return -1;
        }

        public void SetCount(BattleUnitEntity unit, int count)
        {
            var index = GetIndex(unit);
            if (index >= 0)
            {
                queue[index].count = count;
            }
        }
    }




