using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerVO : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip OnStart;
    public AudioClip OnStethoscope;
    public AudioClip OnHeartPuzzlePiece;
    public AudioClip OnSnakePuzzlePiece;
    public AudioClip OnTwoSnakesPuzzlePiece;
    public AudioClip OnPillBottle;
    public AudioClip OnKey;

    public TextMeshProUGUI SubtitlesText;
    public float StartDelayDurationSecs = 4f;
    public float SubtitleDurationSecs = 6f;

    public void Start()
    {
        StartCoroutine(PlayStartVOAfterDelay());
    }

    private IEnumerator PlayStartVOAfterDelay()
    {
        yield return new WaitForSeconds(StartDelayDurationSecs);
        PlayStartVO();
        yield return null;
    }

    private IEnumerator EraseSubtitleAfterDelay()
    {
        yield return new WaitForSeconds(SubtitleDurationSecs);
        SubtitlesText.text = "";
        yield return null;
    }

    public void PlayStartVO()
    {
        AudioSource.clip = OnStart;
        AudioSource.Play();
        SubtitlesText.text = "Where am I? ...how did I get here? ...what is this place?";
        StartCoroutine(EraseSubtitleAfterDelay());
    }

    public void PlayStethoscopeVO()
    {
        AudioSource.clip = OnStethoscope;
        AudioSource.Play();
        SubtitlesText.text = "A... stethoscope?";
        StartCoroutine(EraseSubtitleAfterDelay());
    }

    public void PlayerHeartPuzzlePieceVO()
    {
        AudioSource.clip = OnHeartPuzzlePiece;
        AudioSource.Play();
        SubtitlesText.text = "What is this?";
        StartCoroutine(EraseSubtitleAfterDelay());
    }

    public void PlayOneSnakePuzzlePieceVO()
    {
        AudioSource.clip = OnSnakePuzzlePiece;
        AudioSource.Play();
        SubtitlesText.text = "What is this puzzle piece?";
        StartCoroutine(EraseSubtitleAfterDelay());
    }

    public void PlayTwoSnakesPuzzlePieceVO()
    {
        AudioSource.clip = OnTwoSnakesPuzzlePiece;
        AudioSource.Play();
        SubtitlesText.text = "...a puzzle piece?";
        StartCoroutine(EraseSubtitleAfterDelay());
    }

    public void PlayPillBottleVO()
    {
        AudioSource.clip = OnPillBottle;
        AudioSource.Play();
        SubtitlesText.text = "A pill bottle. ...hmmm... March 17th...";
        StartCoroutine(EraseSubtitleAfterDelay());
    }

    public void PlayKeyVO()
    {
        AudioSource.clip = OnKey;
        AudioSource.Play();
        SubtitlesText.text = "The key!";
        StartCoroutine(EraseSubtitleAfterDelay());
    }
}
