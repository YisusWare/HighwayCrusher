using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallonStageManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    int ballonsGoal;
    [SerializeField]
    TMP_Text ballonsText;
    private int ballonsGotten;
    [SerializeField]
    GameObject prize;
    [SerializeField]
    GameObject loseScreenPanel;
    [SerializeField]
    GameObject GoalSpawnPoint;
    bool RaceEnded = false;
    [SerializeField]
    GameObject substitute;
    void Start()
    {
        loseScreenPanel.transform.localScale = Vector3.zero;
        ballonsGotten = 0;
        ballonsText.text = ballonsGotten + "\\" + ballonsGoal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEndRace()
    {
        if (!RaceEnded)
        {
            if (ballonsGotten >= ballonsGoal)
            {
                int carId = prize.GetComponent<Plains>().carModel.Id;
                SpecialRoadModule road = FindObjectOfType<SpecialRoadModule>();

                bool carIsAvailable = GameManager.instance.IsCarAvailable(carId);

                if (carIsAvailable)
                {
                    GameObject plainInstantiated = Instantiate(substitute, new Vector3(0, GoalSpawnPoint.transform.position.y + 0.5f, 0), Quaternion.identity);
                    plainInstantiated.transform.SetParent(road.transform);
                    return;
                }
                
                Vector2 pointZero = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
                GameObject plain = Instantiate(prize, new Vector3(0, GoalSpawnPoint.transform.position.y + 0.5f, 0), Quaternion.identity);
                
                plain.transform.SetParent(road.transform);
            }
            else
            {
                loseScreenPanel.transform.LeanScale(Vector3.one, 0.3f);
            }
            RaceEnded = true;
        }
        
    }

    public void OnGetBallon(int ballons)
    {
        
        ballonsGotten += ballons;
        ballonsText.text = ballonsGotten + "\\" + ballonsGoal; 
    }

    public void CloseLosePanel()
    {
        loseScreenPanel.transform.LeanScale(Vector3.zero, 0.3f);
        GameManager.instance.specialStage = false;
        GameManager.instance.ResumeGame();
        GameManager.instance.ChangeScene(1);
    }

}
