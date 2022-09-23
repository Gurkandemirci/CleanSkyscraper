using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using ElephantSDK;
using Facebook.Unity;
using MoreMountains.NiceVibrations;

public class UIManager : LocalSingleton<UIManager>
{
    #region FBSDK

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }
    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    #endregion
    #region Progges
    //public ProggesBar proggesBar;
    void Start()
    {
        
        game.levelIndex = PlayerPrefs.GetInt("Level");
        game.level = PlayerPrefs.GetInt("GameLevel");
        if (game.level == 0)
        {
            game.level++;
            PlayerPrefs.SetInt("GameLevel", game.level);
        }
        //game.levelText.text ="Level " + game.level.ToString();
        //proggesBar.fullDistance = GetDistance();
    }
    //private void FixedUpdate()
    //{
    //    float newDistance = GetDistance();
    //    float progressValuve = Mathf.InverseLerp(proggesBar.fullDistance, 0, newDistance);
    //    UpdateProgressFill(progressValuve);
    //}
    //void UpdateProgressFill(float valuve)
    //{
    //    proggesBar.proggresBar.fillAmount = valuve;
    //}
    //private float GetDistance()
    //{
    //    return Vector3.Distance(proggesBar.player.transform.position, proggesBar.finishFlag.transform.position);
    //}
    #endregion
    #region StartPanel
    public Start start;
    public void TapHand()
    {
        Elephant.LevelStarted(game.level);
        start.holdMoveArrow.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.InBack);
        StartCoroutine(SPanel());
        MMVibrationManager.Haptic(HapticTypes.Selection);
    }
    IEnumerator SPanel()
    {
        yield return new WaitForSeconds(.6f);
        DOTween.To(() => game.levelBack.anchoredPosition, x => game.levelBack.anchoredPosition = x, new Vector2(0,-140), .4f).SetEase(Ease.OutBack);

        //game.gamePanel.transform.DOLocalMove(Vector3.zero, .5f).SetEase(Ease.OutBack);
        start.startPanel.SetActive(false);
        yield return new WaitForSeconds(.5f);
        //GameManager.Instance.TutorialHand();
    }
    #endregion
    #region Game
    public Game game;

    public void FruitText()
    {
        game.fruitCount++;
        game.fruitText.text = game.fruitCount.ToString();
    }



    //public void Trap(string text)
    //{
    //    game.dontEatingTrapsText.transform.localScale = Vector3.zero;
    //    game.dontEatingTrapsText.color = game.textColor[0];
    //    game.dontEatingTrapsText.text = text;
    //    game.dontEatingTrapsText.transform.position = game.defPos.position;


    //    game.dontEatingTrapsText.transform.DOScale(Vector3.one,.4f);
    //    game.dontEatingTrapsText.transform.DOMove(game.notificationPos.position, 1f).SetDelay(.6f);
    //    game.dontEatingTrapsText.DOColor(game.textColor[1], 1f).SetDelay(.6f);
    //}
    #endregion
    #region Finish

    public Finish finish;
    public void FinishPanel()
    {
        finish.CleanedRoom.transform.DOLocalMoveY(-100, .4f).SetEase(Ease.OutBounce).SetDelay(1.7f);
        finish.CleanedRoomText.text = PlayerControl.Instance.cleanedRoom.ToString() + " / 30";
        finish.nextPanel.SetActive(true);
        game.levelBack.transform.DOLocalMoveY(1200, .4f).SetEase(Ease.InBounce);
        finish.nextButton.transform.DOScale(Vector3.one, .4f).SetEase(Ease.OutBounce).SetDelay(.7f);
        finish.completed.transform.DOLocalMoveY(350, .6f).SetEase(Ease.OutBounce).SetDelay(1.7f);
        Elephant.LevelCompleted(game.level);
        MMVibrationManager.Haptic(HapticTypes.Success);
    }


    public void NextLevel()
    {
        MMVibrationManager.Haptic(HapticTypes.Success);
        game.levelIndex++;
        game.level++;
        PlayerPrefs.SetInt("Level", game.levelIndex);
        PlayerPrefs.SetInt("GameLevel", game.level);
        if (game.levelIndex >= 7)
        {
            game.levelIndex = 0;
            PlayerPrefs.SetInt("Level", 0);
        }
        SceneManager.LoadScene(1);
    }


    #endregion

    #region Fail
    public Fail fail;
    public void Failed()
    {
        MMVibrationManager.Haptic(HapticTypes.Failure);
        Elephant.LevelFailed(game.level);
        fail.failPanel.SetActive(true);
        game.levelBack.transform.DOLocalMove(new Vector3(0, 1200, 0), .3f).SetEase(Ease.InBounce);
        fail.failText.transform.DOLocalMove(new Vector3(0, 600, 0), .3f).SetEase(Ease.OutBounce).SetDelay(.2f);
        fail.resetButton.transform.DOLocalMove(new Vector3(0, -600, 0), .3f).SetEase(Ease.OutBounce).SetDelay(.7f);
    }
    public void ResetLevel()
    {
        MMVibrationManager.Haptic(HapticTypes.Failure);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion


}
#region Class's
//[System.Serializable]
//public class ProggesBar
//{
//    public Image proggresBar;
//    public GameObject proggres;
//    public GameObject player;
//    public GameObject finishFlag;
//    public float fullDistance;
//}
[System.Serializable]
public class Start
{
    public GameObject startPanel;
    public GameObject holdMoveArrow;
}
[System.Serializable]
public class Game
{
    public int levelIndex;
    public int level;
    public int fruitCount;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI fruitText;
    //public TextMeshProUGUI dontEatingTrapsText;
    //public Transform defPos;
    //public Transform notificationPos;
    //public Color[] textColor;
    //public GameObject tutorialHand;
    public RectTransform levelBack;
}
[System.Serializable]
public class Finish
{
    public GameObject nextButton;
    public GameObject nextPanel;
    public GameObject completed;
    public GameObject CleanedRoom;
    public TMP_Text CleanedRoomText;

}
[System.Serializable]
public class Fail
{
    public GameObject resetButton;
    public GameObject failText;
    public GameObject failPanel;
}
#endregion
