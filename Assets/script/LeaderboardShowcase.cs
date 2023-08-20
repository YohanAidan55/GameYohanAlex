using Dan.Main;
using Dan.Models;
using TMPro;
using UnityEngine;
public class LeaderboardShowcase : MonoBehaviour
{
    [SerializeField] private string _leaderboardPublicKey;
    
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    [SerializeField] private TextMeshProUGUI[] _entryFields;
    
    [SerializeField] private TMP_InputField _playerUsernameInput;

    [SerializeField] private string _leaderboardPublicKeyDynamic = "9f54a4164dab24985688d88f5430a8853db5a26ae593d88f5b79fe83af36aa04";
    [SerializeField] private string _leaderboardPublicKeyClassic = "eaca18c14c7e0a50bc22f52a97ef64d4fbddea5fea00af58d0e1b6d4a98263e8";

    private int _playerScore;
    private bool isSubmit = false;
    
    private void Start()
    {
        _playerScoreText.text = "Score: " + PlayerPrefs.GetInt("score");

        GameObject.Find("UsernameInputField (TMP)").GetComponent<TMP_InputField>().text = PlayerPrefs.GetString("playerActuel");
        isSubmit = false;

        Load();

    }

    public void Load()
    {
        if (PlayerPrefs.GetInt("mode") == 1)
        {
            LeaderboardCreator.GetLeaderboard(_leaderboardPublicKeyClassic, OnLeaderboardLoaded);
        }
        else
        {
            LeaderboardCreator.GetLeaderboard(_leaderboardPublicKeyDynamic, OnLeaderboardLoaded);
        }
    }


    private void OnLeaderboardLoaded(Entry[] entries)
    {
        foreach (var entryField in _entryFields)
        {
            entryField.text = "";
        }

        for (int i = 0; i < entries.Length; i++)
        {
            _entryFields[i].text = $"{i+1}. {entries[i].Username} : {entries[i].Score}";
        }
    }

    public void Submit()
    {
        PlayerPrefs.SetString("playerActuel", _playerUsernameInput.text);

        if (!isSubmit)
        {
            if (PlayerPrefs.GetInt("mode") == 1)
            {
                LeaderboardCreator.UploadNewEntry(_leaderboardPublicKeyClassic, _playerUsernameInput.text, PlayerPrefs.GetInt("score"), Callback);
            }
            else
            {
                LeaderboardCreator.UploadNewEntry(_leaderboardPublicKeyDynamic, _playerUsernameInput.text, PlayerPrefs.GetInt("score"), Callback);
            }
            isSubmit = true;
        }
        
    }
    
    public void DeleteEntry()
    {
        LeaderboardCreator.DeleteEntry(_leaderboardPublicKey, Callback);
    }

    public void ResetPlayer()
    {
        PlayerPrefs.SetString("playerActuel", _playerUsernameInput.text);
        LeaderboardCreator.ResetPlayer();
    }
    
    private void Callback(bool success)
    {
        if (success)
        {
            Load();
        }
            
    }
}
