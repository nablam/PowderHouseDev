using System.Collections.Generic;

public class BHG_TaskSystem
{


    private List<ITaskAction> taskList;

    private ITaskAction _curTask = null;
    public BHG_TaskSystem()
    {
        taskList = new List<ITaskAction>();
    }


    public ITaskAction getCurTak()
    {

        return _curTask;
    }
    public ITaskAction RequestNextTask()
    {
        if (taskList.Count > 0)
        {
            ITaskAction task = taskList[0];
            _curTask = task;
            taskList.RemoveAt(0);
            return task;
        }
        else
        {
            // No tasks are available
            return null;
        }
    }

    public void AddTask(ITaskAction task)
    {
        taskList.Add(task);
    }

}
