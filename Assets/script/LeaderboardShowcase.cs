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

    private int _playerScore;
    
    private void Start()
    {
        _playerScoreText.text = "Score: " + PlayerPrefs.GetInt("score");
        Load();
    }

    public void Load()
    {
        LeaderboardCreator.GetLeaderboard(_leaderboardPublicKey, OnLeaderboardLoaded);
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
        LeaderboardCreator.UploadNewEntry(_leaderboardPublicKey, _playerUsernameInput.text, PlayerPrefs.GetInt("score"), Callback);
    }
    
    public void DeleteEntry()
    {
        LeaderboardCreator.DeleteEntry(_leaderboardPublicKey, Callback);
    }

    public void ResetPlayer()
    {
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
