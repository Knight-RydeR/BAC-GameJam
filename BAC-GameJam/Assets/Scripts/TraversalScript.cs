using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraversalScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> tilePositions;
    [SerializeField] private List<GameObject> blueEndPath;
    [SerializeField] private List<GameObject> redEndPath;
    [SerializeField] private List<GameObject> greenEndPath;
    [SerializeField] private List<GameObject> yellowEndPath;
    [SerializeField] private TMPro.TMP_Text rolledNumber;
    [SerializeField] private GameObject diceObject;
    [SerializeField] private GameObject targetGameObject;

    private int[] rotationValuesForDie;
    private int myRolledNumber;
    //RED = GREEN = YELLOW = BLUE (Order of tiles)
    // Start is called before the first frame update
    void Start()
    {
        rotationValuesForDie = new int[18];
        rotationValuesForDie[0] = 270;
        rotationValuesForDie[1] = 0;
        rotationValuesForDie[2] = 0;
        rotationValuesForDie[3] = 360;
        rotationValuesForDie[4] = 90;
        rotationValuesForDie[5] = -90;
        rotationValuesForDie[6] = 360;
        rotationValuesForDie[7] = 90;
        rotationValuesForDie[8] = 0;
        rotationValuesForDie[9] = 360;
        rotationValuesForDie[10] = 90;
        rotationValuesForDie[11] = 90;
        rotationValuesForDie[12] = 90;
        rotationValuesForDie[13] = 90;
        rotationValuesForDie[14] = 90;
        rotationValuesForDie[15] = 180;
        rotationValuesForDie[16] = 0;
        rotationValuesForDie[17] = 0;
        //Dice rotation with 1 up: 270 0 0
        //Dice rotation with 2 up: 360 90 -90
        //Dice rotation with 3 up: 360 90 0
        //Dice rotation with 4 up: 360 90 90
        //Dice rotation with 5 up: 90 90 90
        //Dice rotation with 6 up: 180 0 0
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int findMyIndex(GameObject tile) {
        int indexRecovered = 0;
        for (int i = 0; i < tilePositions.Count; i++) {
            if (tilePositions[i] == tile) {
                indexRecovered = i;
                break;
            }
        }
        return indexRecovered;
    }

    public void rollDice() {
        // diceObject.GetComponent<Animation>().Play("DieSpinning");
        rolledNumber.text = "?";
        StartCoroutine(delayBeforeRevealDice());
    }

    IEnumerator delayBeforeRevealDice() {
        diceObject.GetComponent<Animation>().Play("DieSpinning");
        yield return new WaitForSeconds(2f);
        stopDiceRoll();
    }

    public void stopDiceRoll() {
        diceObject.GetComponent<Animation>().Stop();
        int rollNumber = Random.Range(0, 6);
        // Quaternion newRot = Quaternion.Euler(rotationValuesForDie[rollNumber  * 3], rotationValuesForDie[rollNumber  * 3 + 1], rotationValuesForDie[rollNumber  * 3 + 2]);
        diceObject.transform.eulerAngles = new Vector3(
            rotationValuesForDie[rollNumber  * 3],
            rotationValuesForDie[rollNumber * 3 + 1],
            rotationValuesForDie[rollNumber * 3 + 2]
        );
        rolledNumber.text = (rollNumber + 1) + "";
        // diceObject.transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 2 * Time.deltaTime);
        Debug.Log("Dice rolled: " + (rollNumber + 1));
        myRolledNumber = rollNumber + 1;
    }

    public int getRolledNumber() {
        int temp = myRolledNumber;
        myRolledNumber = 0;
        return temp;
    }
}
