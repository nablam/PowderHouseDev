using System.Collections.Generic;
using UnityEngine;

public class BHG_TaskSystem
{
    public class BHG_Task
    {
        public Vector3 targetPosition;
    }

    private List<BHG_Task> taskList;

    public BHG_TaskSystem()
    {
        taskList = new List<BHG_Task>();
    }

    public BHG_Task RequestNextTask()
    {
        if (taskList.Count > 0)
        {
            // Give the first task
            BHG_Task task = taskList[0];
            taskList.RemoveAt(0);
            return task;
        }
        else
        {
            // No tasks are available
            return null;
        }
    }

    public void AddTask(BHG_Task task)
    {
        taskList.Add(task);
    }

}
