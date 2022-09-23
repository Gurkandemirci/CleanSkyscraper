using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Obi;
using DG.Tweening;
using UnityEngine.Animations.Rigging;

public class PlayerControl : LocalSingleton<PlayerControl>
{
    public PlayerParametres player;
    public GameParametres game;
    public GameFinish gameFinish;
    public LayerMask mask;
    public float speed;
    public bool gameOn = true;
    public bool isSameLevel = false;
    public bool checkTurn = true;

    public int cleanedRoom = 0;

    float posY;
    void Start()
    {
        AnimationManager.Instance.PlayAnim(AnimationManager.Instance.character1, AnimationManager.Instance.idle);
        AnimationManager.Instance.PlayAnim(AnimationManager.Instance.character2, AnimationManager.Instance.idle);
    }
    void Update()
    {
        if (gameOn)
        {
            Control();
        }
        
    }
    public void RayCharacter(Transform activeCharacter,Transform otherCharacter)
    {
        RaycastHit hit;

        if(activeCharacter.position.x > -5f && activeCharacter.position.x < 5f)
        {
            if(activeCharacter.position.y < otherCharacter.position.y)
            {
                game.rotateTransform = GetActiveCharacter(player.leftright);
            }
            
        }

        else
        {
            Check(player.leftright);
        }
    }
    public void Check(bool lR)
    {
        if (lR)
        {
            GameLose(player.characterLeft, player.characterRight);
        }
        else
        {
            GameLose(player.characterRight, player.characterLeft);
        }
    }
    void GameLose(Transform character,Transform activeCharacter)
    {
        character.SetParent(null);
        player.rope.GetComponent<AttachReferance>().attach1.enabled = false;
        player.rope.GetComponent<AttachReferance>().attach2.enabled = false;
        character.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        activeCharacter.GetComponent<RigBuilder>().enabled = false;
        gameOn = false;
        AnimationManager.Instance.PlayAnim(activeCharacter.GetComponent<AnimancerComponent>(), AnimationManager.Instance.falling);     //Okey Düþme animasyonu
        activeCharacter.DOMoveY(0,3);
        UIManager.Instance.Failed();
    }
    public void GameFinish()
    {
        UIManager.Instance.FinishPanel();
        gameOn = false;
        player.rope.GetComponent<AttachReferance>().attach1.enabled = false;
        player.rope.GetComponent<AttachReferance>().attach2.enabled = false;
        Character(player.characterLeft, gameFinish.characterLeft);
        Character(player.characterRight, gameFinish.characterRight);
        Camera.main.transform.DOMove(gameFinish.cameraPos.position, 1);
        Camera.main.transform.DORotateQuaternion(gameFinish.cameraPos.rotation, 1);
    }
    void Character(Transform character,Transform movePos)
    {
        character.GetComponent<RigBuilder>().enabled = false;
        character.DOLookAt(movePos.position, .1f);
        AnimationManager.Instance.PlayAnim(character.GetComponent<AnimancerComponent>(), AnimationManager.Instance.jumping);       //Oyun kazanýnca yere atlama animasyonu
        character.DOMove(movePos.position, 1).OnComplete(() =>
        {
            AnimationManager.Instance.PlayAnim(character.GetComponent<AnimancerComponent>(), AnimationManager.Instance.dance);  
            character.DORotateQuaternion(movePos.rotation, .5f);
            GameManager.Instance.moneyText.gameObject.SetActive(true);
            UIManager.Instance.FinishPanel();
        });
    }
    public void Control()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            isSameLevel = false;
            RayCharacter(game.unRotateTransform,game.rotateTransform);
            if (!game.isStart)
            {
                posY = transform.position.y;
                UIManager.Instance.TapHand();
                game.isStart = true;
            }
            else if(game.rotateTransform.position.y > game.unRotateTransform.position.y)
            {
                game.rotate = -game.rotate;
            }
            
        }
        if (game.isStart) //&& game.rotateTransform.position.y < game.unRotateTransform.position.y
        {
            game.rotateTransform.Rotate(-game.rotate * Time.deltaTime);
        }



        if (player.leftright)
        {
            player.leftParent.position = player.characterLeft.position;
        }
        else if (!player.leftright)
        {
            player.rightParent.position = player.characterRight.position;
        }

        /*if (Mathf.Abs(game.rotateTransform.position.y - game.unRotateTransform.position.y) < 0.1f && game.isStart)
            isSameLevel = true;*/

        if (checkTurn)
        {
            if (Mathf.Abs(game.rotateTransform.position.y - game.unRotateTransform.position.y) < 0.5f && game.rotate.x > 0 &&
                        game.rotateTransform.position.x < game.unRotateTransform.position.x)
            {
                game.rotate = -game.rotate;
            }

            else if (Mathf.Abs(game.rotateTransform.position.y - game.unRotateTransform.position.y) < 0.5f && game.rotate.x < 0 &&
                game.rotateTransform.position.x > game.unRotateTransform.position.x)
            {
                game.rotate = -game.rotate;
            }
        }

        
    }
    public Transform GetActiveCharacter(bool _leftRight)
    {
        player.leftright = !player.leftright;
        game.rotate = -game.rotate;
        if (_leftRight)
        {
            AnimationManager.Instance.PlayAnim(AnimationManager.Instance.character1, AnimationManager.Instance.turnIdle);               //Tutunan
            AnimationManager.Instance.PlayAnim(AnimationManager.Instance.character2, AnimationManager.Instance.unTurnCharacter);    //Dönen 
            player.characterLeft.GetComponent<Collider>().enabled = true;
            player.characterRight.GetComponent<Collider>().enabled = false;
            player.characters.SetParent(player.leftParent);
            game.unRotateTransform = player.rightParent;
            return player.leftParent;
        }
        else
        {
            AnimationManager.Instance.PlayAnim(AnimationManager.Instance.character2, AnimationManager.Instance.turnIdle);
            AnimationManager.Instance.PlayAnim(AnimationManager.Instance.character1, AnimationManager.Instance.unTurnCharacter);
            player.characterLeft.GetComponent<Collider>().enabled = false;
            player.characterRight.GetComponent<Collider>().enabled = true;
            player.characters.SetParent(player.rightParent);
            game.unRotateTransform = player.leftParent;
            return player.rightParent;
        }
    }
}
[System.Serializable]
public class PlayerParametres
{
    public bool leftright;
    public Transform leftParent;
    public Transform rightParent;
    public Transform characters;
    public Transform characterLeft;
    public Transform characterRight;
    public GameObject rope;
}
[System.Serializable]
public class GameParametres
{
    public bool isStart;
    public Vector3 rotate;
    public Transform rotateTransform;
    public Transform unRotateTransform;
}
[System.Serializable]
public class GameFinish
{
    public Transform characterLeft;
    public Transform characterRight;
    public Transform cameraPos;
}
