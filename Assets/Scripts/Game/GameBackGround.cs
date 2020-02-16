using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

public class GameBackGround : MonoBehaviour,IGameSystem,ITriggerEnterReceiver
{
    [Range(1.0f,200.0f)]
    public float speed;

    [Header("Connectors")]
    public ResourceConnector resourceConnector;
    public GameSessionConnector gameSessionConnector;
    public GameStateConnector gameStateConnector;
    public JobSystemConnector jobSystemConnector;

    private GameBackGroundComponent bgComp1, bgComp2, bgComp3;
    private GameObject container;

    private GameObject bgTemplate;

    public string GetComponentName => gameObject.name;

    private Vector3 startPosition;
    public void Initilize()
    {

        container = new GameObject("BGContainer");
        container.transform.SetParent(transform);
        bgTemplate = resourceConnector.GetLoadedPrefab(SystemType.bgComponent);
       
        GameObject  bg = Instantiate(bgTemplate, Vector3.zero, Quaternion.identity, container.transform);
        bgComp1 = bg.GetComponent<GameBackGroundComponent>();
        bgComp1.SetBG(bgComp1.spriteRenderer.sprite);

        bg = Instantiate(bgTemplate, bgComp1.nextSpawnPoint.position, Quaternion.identity,container.transform);
        bgComp2 = bg.GetComponent<GameBackGroundComponent>();
        bgComp2.SetBG(bgComp2.spriteRenderer.sprite);

        bg = Instantiate(bgTemplate, bgComp2.nextSpawnPoint.position, Quaternion.identity, container.transform);
        bgComp3 = bg.GetComponent<GameBackGroundComponent>();
        bgComp3.SetBG(bgComp1.spriteRenderer.sprite);

        gameStateConnector.RegisterListener(OnGameStateChange);
        speed = 0;
        

    }
    private void NextTrigger()
    {

        bgComp1.transform.position = bgComp3.nextSpawnPoint.position;
        bgComp1.SetBG(bgComp1.spriteRenderer.sprite);

        GameBackGroundComponent temp = bgComp2;
        GameBackGroundComponent temp1 = bgComp3;
        bgComp2 = bgComp1;
        bgComp3 = temp;
        bgComp1 = temp1;

    }
    private void OnGameStateChange(GameState State, GameState prevState)
    {
        switch (State)
        {
            
            case GameState.Running:
                startPosition = container.transform.position;
                speed = gameSessionConnector.GetSessionData().levelDesignData.playerStats.MovementSpeed;
                jobSystemConnector.registerCountDown(1.0f, SendDistanceTravelled);
                break;
            case GameState.GameOver:
                speed = 0;
                break;
          
        }
    }
    private void SendDistanceTravelled(bool done)
    {
        float currentDistance = (container.transform.position - startPosition).sqrMagnitude;
        int travelled = Mathf.RoundToInt(currentDistance * 0.001f);//1/10000
        gameSessionConnector.OnPlayerActivityChange(PlayerActivity.DistanceTravel, travelled);
        jobSystemConnector.registerCountDown(1.0f, SendDistanceTravelled);
    }
    private void Update()
    {
        container.transform.position = container.transform.position + Vector3.down * Time.deltaTime * speed;
    }

    public void DeInitilize()
    {
        gameStateConnector.UnRegisterListener(OnGameStateChange);
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    public void OnITriggerEnter(Entity.Entity collidedEntity,string fromComponent)
    {
        
        if(collidedEntity is PlayerEntity)
        {
            NextTrigger();
        }
    }
}
