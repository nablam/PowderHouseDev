using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class BHGA_CorrectFloor : MonoBehaviour
{

    IEnumerator MoveObjA2B(Transform argtran)
    {
        Debug.Log("Move from a to b finish when reached b");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Done!");
    }
    public async void Start()
    {


        await LoadModelAsync();

        Debug.Log("finnished");
    }


    async Task LoadModelAsync()
    {
        for (int x = 0; x < 999999999; x++)
        {


        }
        print("Done");
    }


    public IEnumerator MoveAtSpeedCoroutine(Transform argt, Vector3 end, float speed)
    {
        //while you are far enough away to move
        while (Vector3.Distance(argt.position, end) > speed)
        {
            //MoveTowards the end position by a given distance
            argt.position = Vector3.MoveTowards(argt.position, end, speed);
            //wait for a frame and repeat
            yield return 0;
        }
        //Since you are really really close, now you can just go to the final position.
        this.transform.position = end;
    }




    /*
    public async void Start()
    {
        // Wait one second
        await new WaitForSeconds(1.0f);

        // Wait for IEnumerator to complete
        await CustomCoroutineAsync();

        await LoadModelAsync();

        // You can also get the final yielded value from the coroutine
        var value = (string)(await CustomCoroutineWithReturnValue());
        // value is equal to "asdf" here

        // Open notepad and wait for the user to exit
        var returnCode = await Process.Start("notepad.exe");

        // Load another scene and wait for it to finish loading
        await SceneManager.LoadSceneAsync("scene2");
    }

    async Task LoadModelAsync()
    {
        var assetBundle = await GetAssetBundle("www.my-server.com/myfile");
        var prefab = await assetBundle.LoadAssetAsync<GameObject>("myasset");
        GameObject.Instantiate(prefab);
        assetBundle.Unload(false);
    }

    async Task<AssetBundle> GetAssetBundle(string url)
    {
        return (await new WWW(url)).assetBundle
    }

    IEnumerator CustomCoroutineAsync()
    {
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator CustomCoroutineWithReturnValue()
    {
        yield return new WaitForSeconds(1.0f);
        yield return "asdf";
    }
    */
}
public static class AwaitExtensions
{
    public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
    {
        return Task.Delay(timeSpan).GetAwaiter();
    }
}