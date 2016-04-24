using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Util;
using Sfs2X.Requests;
using Sfs2X.Controllers;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Sfs2X.Logging;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;

public class multiplayer : MonoBehaviour
{
    SmartFox sfs;
    private string defaultHost = "54.183.219.67";
    private int defaultTcpPort = 9933;
    private int defaultWsPort = 8888;
    string username = "lol2";
    public Text te;
    public GameObject playerModel;
    private Dictionary<SFSUser, GameObject> remotePlayers = new Dictionary<SFSUser, GameObject>();      
    private GameObject localPlayer;

    private GameObject[] zombies;
    private Dictionary<String, GameObject> zombiesMine;
    private Dictionary<String, GameObject> zombiesTheirs;
    private playerbeacon localPlayerController;
    public Text text1;
    public Text text2;

    void Start ()
    {
 SpawnLocalPlayer();
        username = "guest" + UnityEngine.Random.Range(0, 1000).ToString();
        sfs = new SmartFox();
        //sfs.Debug = true;
        sfs.AddEventListener(SFSEvent.CONNECTION, onConnection);
        sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
        sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
        sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
        sfs.AddEventListener(SFSEvent.PUBLIC_MESSAGE, OnPublicMessage);
        sfs.AddEventListener(SFSEvent.OBJECT_MESSAGE, OnObjectMessage);
        sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
        sfs.AddEventListener(SFSEvent.USER_VARIABLES_UPDATE, OnUserVariableUpdate);
        sfs.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoom);
        sfs.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
        sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, onRoomJoinError);
        ConfigData cfg = new ConfigData();
        cfg.Host = defaultHost;
        cfg.Port = defaultTcpPort;
        cfg.Zone = "BasicExamples";
        cfg.Debug = true;
        sfs.Connect(cfg);
       
    }

    private void onRoomJoinError(BaseEvent evt)
    {
        te.text += "enterRoomError/n";
        te.text += evt.Params["reason"];
    }

    private void OnUserEnterRoom(BaseEvent evt)
    {
        te.text += "enteredroom/n";
        if (localPlayer != null)
        {
            List<UserVariable> userVariables = new List<UserVariable>();
            userVariables.Add(new SFSUserVariable("x", (double)localPlayer.transform.position.x));
            userVariables.Add(new SFSUserVariable("y", (double)localPlayer.transform.position.y));            
            userVariables.Add(new SFSUserVariable("rot", (double)localPlayer.transform.rotation.eulerAngles.y));
            userVariables.Add(new SFSUserVariable("shot", (int)0));
            //userVariables.Add(new SFSUserVariable("team", sfs.MySelf.GetVariable("team").GetIntValue()));
            sfs.Send(new SetUserVariablesRequest(userVariables));
        }
    }

    private void OnUserExitRoom(BaseEvent evt)
    {
        SFSUser user = (SFSUser)evt.Params["user"];
        RemoveRemotePlayer(user);
    }

    private void RemoveRemotePlayer(SFSUser user)
    {
        if (user == sfs.MySelf)
        {
            return;
        }
        if (remotePlayers.ContainsKey(user))
        {
            Destroy(remotePlayers[user]);
            remotePlayers.Remove(user);
        }
    }

    void OnApplicationQuit()
    {
        RemoveLocalPlayer();
        if (sfs.IsConnected)
        {
            sfs.Disconnect();
        }
    }

    private void OnUserVariableUpdate(BaseEvent evt)
    {
        #if UNITY_WSA && !UNITY_EDITOR
		List<string> changedVars = (List<string>)evt.Params["changedVars"];
        #else
        ArrayList changedVars = (ArrayList)evt.Params["changedVars"];
        #endif
        SFSUser user = (SFSUser)evt.Params["user"];
        if (user == sfs.MySelf) return;
        if (!remotePlayers.ContainsKey(user))
        {
            Vector3 pos = new Vector3(0, 1, 0);
            if (user.ContainsVariable("x") && user.ContainsVariable("y"))
            {
                pos.x = (float)user.GetVariable("x").GetDoubleValue();
                pos.y = (float)user.GetVariable("y").GetDoubleValue();
            }
            float rotAngle = 0;
            if (user.ContainsVariable("rot"))
            {
                rotAngle = (float)user.GetVariable("rot").GetDoubleValue();
            }
            SpawnRemotePlayer(user, pos, Quaternion.Euler(0, rotAngle, 0));
        }
        if (changedVars.Contains("x") && changedVars.Contains("y") && changedVars.Contains("rot"))
        {
            remotePlayers[user].GetComponent<SimpleRemoteInterpolation>().SetTransform(
            new Vector3((float)user.GetVariable("x").GetDoubleValue(), (float)user.GetVariable("y").GetDoubleValue(), (float)0),
            Quaternion.Euler(0, 0, (float)user.GetVariable("rot").GetDoubleValue()), true);
        }
        
        
        if (changedVars.Contains("shot"))
        {
            remotePlayers[user].GetComponent<shoot>().Fire();
        }
    }

    private void SpawnLocalPlayer()
    {
        Vector3 pos = new Vector3(-93, 0, 0);
        Quaternion rot = new Quaternion(0,0,0,0);
        if (localPlayer != null)
        {
            pos = localPlayer.transform.position;
            rot = localPlayer.transform.rotation;
            Camera.main.transform.parent = null;
            Destroy(localPlayer);
        }
        else
        {
            pos = new Vector3(-93, 0, 0);
            rot = Quaternion.identity;
        }
        localPlayer = Instantiate(playerModel, pos,rot) as GameObject;
        localPlayer.transform.position = pos;
        localPlayer.transform.rotation = rot;
        localPlayer.AddComponent<playerbeacon>();
        localPlayer.AddComponent<player>();
        localPlayer.GetComponent<shoot>().local = true;
        localPlayer.GetComponent<player>().seteverything(localPlayer, 20, 30, 3, 1, text1, text2);
        localPlayerController = localPlayer.GetComponent<playerbeacon>();
        //localPlayer.GetComponentInChildren<TextMesh>().text = sfs.MySelf.Name;
        Camera.main.GetComponent<camerafollow>().go = Camera.main.gameObject;
        Camera.main.GetComponent<camerafollow>().follow = localPlayer;
        /*
        List<UserVariable> userVariables = new List<UserVariable>();
        userVariables.Add(new SFSUserVariable("team", numModel));
        userVariables.Add(new SFSUserVariable("mat", numMaterial));
        sfs.Send(new SetUserVariablesRequest(userVariables));
        */
    }

    void spawnZombie()
    {
        //sfs.AddEventListener(SFSEvent.USER_VARIABLES_UPDATE, OnUserVarsUpdate);
        // Create some User Variables
        List<UserVariable> zombie = new List<UserVariable>();
        zombie.Add(new SFSUserVariable("zombie", ""));
        zombie.Add(new SFSUserVariable("country", "Sweden"));
        zombie.Add(new SFSUserVariable("x", 10));
        zombie.Add(new SFSUserVariable("y", 5));
        sfs.Send(new SetUserVariablesRequest(zombie));
    }


    private void OnObjectMessage(BaseEvent evt)
    {
        ISFSObject dataObj = (SFSObject)evt.Params["message"];
        SFSUser sender = (SFSUser)evt.Params["sender"];
        if (dataObj.ContainsKey("cmd"))
        {
            switch (dataObj.GetUtfString("cmd"))
            {
                case "rm":
                    Debug.Log("Removing player unit " + sender.Id);
                    RemoveRemotePlayer(sender);
                    break;
            }
        }
    }

    private void OnPublicMessage(BaseEvent evt)
    {
        te.text += (string)evt.Params["message"];
    }

    private void OnLoginError(BaseEvent evt)
    {        
        Debug.Log("login failed");
    }

    private void OnLogin(BaseEvent evt)
    {
        
        Debug.Log("logged in succeeded");
        te.text += "loggedin/n";
        te.text += sfs.RoomList.Count.ToString();
        if (sfs.RoomList.Count > 0)
        {            
            sfs.Send(new Sfs2X.Requests.JoinRoomRequest(sfs.RoomList[0].Name));
            te.text += "joining " + sfs.RoomList[0].Name + "\n";
        }
    }

    private void OnConnectionLost(BaseEvent evt)
    {
        Debug.Log("Connection was lost; reason is: " + (string)evt.Params["reason"]);
        te.text += "connection lost";
    }

    private void onConnection(BaseEvent evt)
    {
        if ((bool)evt.Params["success"])
        {
            te.text += "success";
            Debug.Log("Connection established successfully");
            Debug.Log("Connection mode is: " + sfs.ConnectionMode);
            sfs.Send(new Sfs2X.Requests.LoginRequest(username));            
        }
        else
        {
            Debug.Log("Connection failed; is the server running at all?");
            te.text += "failed";
            reset();
        }
    }

    public void reset()
    {
        //sfs.Disconnect();
        sfs.RemoveAllEventListeners();
    }

    void FixedUpdate ()
    {
        if (sfs != null)
            sfs.ProcessEvents();
        if (localPlayer != null && localPlayerController != null && localPlayerController.MovementDirty)
        {
            List<UserVariable> userVariables = new List<UserVariable>();
            userVariables.Add(new SFSUserVariable("x", (double)localPlayer.transform.position.x));
            userVariables.Add(new SFSUserVariable("y", (double)localPlayer.transform.position.y));
            userVariables.Add(new SFSUserVariable("rot", (double)localPlayer.transform.rotation.eulerAngles.z));
            sfs.Send(new SetUserVariablesRequest(userVariables));
            localPlayerController.MovementDirty = false;
        }
        if (localPlayer != null && localPlayer.GetComponent<shoot>().shotdirty)
        {
            List<UserVariable> userVariables = new List<UserVariable>();
            userVariables.Add(new SFSUserVariable("shot", localPlayer.GetComponent<shoot>().shotcount));
            sfs.Send(new SetUserVariablesRequest(userVariables));
            localPlayer.GetComponent<shoot>().shotdirty = false;
        }
    }

    private void SpawnRemotePlayer(SFSUser user, Vector3 pos, Quaternion rot)
    {
        if (remotePlayers.ContainsKey(user) && remotePlayers[user] != null)
        {
            Destroy(remotePlayers[user]);
            remotePlayers.Remove(user);
        }
        GameObject remotePlayer = GameObject.Instantiate(playerModel) as GameObject;
        remotePlayer.AddComponent<SimpleRemoteInterpolation>();
        remotePlayer.GetComponent<SimpleRemoteInterpolation>().SetTransform(pos, rot, false);   
        remotePlayers.Add(user, remotePlayer);
    }

    private void RemoveLocalPlayer()
    {
        SFSObject obj = new SFSObject();
        obj.PutUtfString("cmd", "rm");
        sfs.Send(new ObjectMessageRequest(obj, sfs.LastJoinedRoom));
    }


}
