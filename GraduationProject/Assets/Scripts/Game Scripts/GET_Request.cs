using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;





public class GET_Request : MonoBehaviour
{
    private const string URL = "http://fb5a048099aa.ngrok.io/games";
    [System.Serializable]
    public struct level { public string id;  };
    [System.Serializable]
    public struct game { public string id; public string name;  public level [] Levels;  };
    [System.Serializable]
    public struct game_list { public game[] data ; };
    [System.Serializable]
    public struct test { public game_list y; };
  //  game  g;
    game_list gg;

    test t;
    void Start()
    {
        StartCoroutine(GetRequest());
    }
    public void Get_Button()
    {
        StartCoroutine(GetRequest());
    }

    IEnumerator GetRequest()
    {
        using (UnityWebRequest R = UnityWebRequest.Get(URL))
        {
           // R.SetRequestHeader("hatem", "mohamed"); 
            yield return R.SendWebRequest();

           // string[] pages = URL.Split('/');
           // int page = pages.Length - 1;
         
            if (R.isHttpError)
            {
                Debug.Log(R.error);
            }
            else if ( R.isDone )
            {
              
                Debug.Log(R.downloadHandler.text);
                
                  gg = JsonUtility.FromJson<game_list>(R.downloadHandler.text);
                  Debug.Log(gg.data[0].id + "," + gg.data[0].name +"," +gg.data[0].Levels[0].id +"," +gg.data[0].Levels[1].id + gg.data.Length );
                  Debug.Log(gg.data[1].id + "," + gg.data[1].name + "," + gg.data[1].Levels[0].id );
                //Debug.Log(t.y.Games[0].id);
            }
        }  
    }
}
