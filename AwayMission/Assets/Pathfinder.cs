using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navigation {
    public class Pathfinder : MonoBehaviour
    { // This class is our controller script for player related use of pathfinding in our project.

        CharacterController currentCharacter;

        [Header("Pathfinding Varibles Movement")]
        public GameObject startPosition;
        public GameObject desiredPosition;

        public List<GameObject> tilesToCheck = new List<GameObject>(); // What tiles we want to check the state of its neighbours
        public List<GameObject> checkedTiles = new List<GameObject>();
        public List<GameObject> reachableTiles = new List<GameObject>(); //AI Go through this list and pick the one with the lowest value. 
        public List<GameObject> chosenPathTiles = new List<GameObject>();

        public int possibleMoveDistance;
        public float rotationSpeed = 45;
        public float moveSpeed = 2f;
        float wantedDir;
        float directionToTurn;
        public Vector3 positionToMove;

        string WhichStepIsBroken = "Declare";


        //Rotate to face the direction we need to walk.

        public void DeclarePathfindingConditions(GameObject creatureTokenPicked)
        {
            WhichStepIsBroken = "Declare";
            //currentCharacter = creatureTokenPicked;
            possibleMoveDistance = 1f; //creatureTokenPicked.GetComponent<CreatureToken>().currentMoveDistance;
            startPosition = currentCharacter.GetComponent<CharacterController>().currentPosition;
            tilesToCheck.Add(startPosition);
            //Old Crest Code
            //LCScript.participants[LCScript.currentTurnParticipant].GetComponent<Player>().moveCrestPoints / chosenPiece.GetComponent<CreatureToken>().moveCost;
            EstablishPossibleMoves("CheckPossibleMoves");
        }


        #region PathMovementCode
        public void EstablishPossibleMoves(string checkWhat)
        {
            if (checkWhat == "CheckPossibleMoves")
            {
                WhichStepIsBroken = "CheckPossibleMoves";
                if (tilesToCheck.Count != 0)
                {
                    tilesToCheck[0].GetComponent<GridScript>().FindPossibleMovements();
                }
                else if (tilesToCheck.Count == 0)
                {
                    if (reachableTiles.Count != 0)
                    {
                        possibleToMove = true;
                    }
                    else
                    {
                        Debug.Log("Not Possible TO move ");
                        possibleToMove = false;
                    }
                }
            }
            else if (checkWhat == "ShowPossibleMoves")
            {
                for (int i = 0; i < reachableTiles.Count; i++)
                {
                    reachableTiles[i].GetComponent<GridScript>().ShowPossibleMovements();
                }
            }

            else if (checkWhat == "FindPath")
            {
                WhichStepIsBroken = "FindPath";
                if (tilesToCheck.Count != 0)
                {
                    tilesToCheck[0].GetComponent<GridScript>().FindPossiblePathToStart();
                }
                else if (tilesToCheck.Count == 0)
                {
                    StartCoroutine("ResetBoard", "ShowOnlyPath");
                    StartCoroutine("MovePieceThroughPath");
                }
            }

        }

        IEnumerator MovePieceThroughPath()
        {
            WhichStepIsBroken = "MovePiece";
            // Check the size of chosenPath, if its 0 then we are next to desired position else  we pick the tile closest to start being the last one added so listName.count
            GameObject currentPos = chosenPiece.GetComponent<CreatureToken>().myBoardLocation;
            GameObject desiredPos = null;

            if (chosenPathTiles.Count == 0)
            {
                desiredPos = desiredPosition;
                positionToMove = new Vector3(desiredPos.transform.position.x, chosenPiece.transform.position.y, desiredPos.transform.position.z);
            }
            else if (chosenPathTiles.Count > 0)
            {
                desiredPos = chosenPathTiles[chosenPathTiles.Count - 1];
                positionToMove = new Vector3(desiredPos.transform.position.x, chosenPiece.transform.position.y, desiredPos.transform.position.z);
            }

            //Find where the next tile in the path is in relation to us.
            NextTileLocation(desiredPos, currentPos);

            //Rotate to Face it. Turn Back On When 3D Models are added for now sprites dont need to rotate as they are billboard sprites so this just slows the game down considerabley.
            //Also Rotation is bugged and still does not rotate in the shortest direction some times doing a 270 degreee turn rather than 90.

            // yield return StartCoroutine("Rotation");

            //Move Towards It
            yield return StartCoroutine("WalkToTile");

            if (chosenPiece.GetComponent<CreatureToken>().myBoardLocation == desiredPosition)
            {
                HasMoved();
            }
            else if (chosenPiece.GetComponent<CreatureToken>().myBoardLocation != desiredPosition)
            {
                chosenPathTiles.Remove(chosenPathTiles[chosenPathTiles.Count - 1]);
                StartCoroutine("MovePieceThroughPath");
            }

        }

        void NextTileLocation(GameObject desiredPos, GameObject currentPos)
        {
            //Where we want to face
            if (desiredPos.transform.position.x > currentPos.transform.position.x)
            {
                wantedDir = 90;
            }

            if (desiredPos.transform.position.x < currentPos.transform.position.x)
            {
                wantedDir = 270;
            }

            if (desiredPos.transform.position.z > currentPos.transform.position.z)
            {
                wantedDir = 0f;
            }

            if (desiredPos.transform.position.z < currentPos.transform.position.z)
            {
                wantedDir = 180;
            }

            //Where we currenly face & how we get to face wantedir;
            rotationSpeed = 45;
            float value = rotationSpeed;
            switch (chosenPiece.transform.eulerAngles.y)
            {
                case 0:
                    switch (wantedDir)
                    {
                        case 90:
                            rotationSpeed = value;
                            break;
                        case 270:
                            rotationSpeed = -value;
                            break;
                        case 180:
                            int dir = Random.Range(1, 2);
                            if (dir == 1)
                            {
                                rotationSpeed = -value;
                            }
                            else
                            {
                                rotationSpeed = value;
                            }
                            break;
                    }
                    break;
                case 90:
                    switch (wantedDir)
                    {
                        case 0:
                            rotationSpeed = -value;
                            break;
                        case 270:
                            int dir = Random.Range(1, 2);
                            if (dir == 1)
                            {
                                rotationSpeed = -value;
                            }
                            else
                            {
                                rotationSpeed = value;
                            }

                            break;
                        case 180:
                            rotationSpeed = value;
                            break;
                    }
                    break;
                case 180:
                    switch (wantedDir)
                    {
                        case 90:
                            rotationSpeed = -value;
                            break;
                        case 270:
                            rotationSpeed = value;
                            break;
                        case 0:
                            int dir = Random.Range(1, 2);
                            if (dir == 1)
                            {
                                rotationSpeed = value;
                            }
                            else
                            {
                                rotationSpeed = -value;
                            }
                            print(dir);
                            break;
                    }
                    break;
                case 270:
                    switch (wantedDir)
                    {
                        case 90:
                            int dir = Random.Range(1, 2);
                            if (dir == 1)
                            {
                                rotationSpeed = -value;
                            }
                            else
                            {
                                rotationSpeed = -value;
                            }
                            break;
                        case 0:
                            rotationSpeed = value;
                            break;
                        case 180:
                            rotationSpeed = -value;
                            break;
                    }
                    break;
            }

        }

        IEnumerator WalkToTile()
        {
            while (chosenPiece.transform.position != (positionToMove))
            {
                chosenPiece.transform.position = Vector3.MoveTowards(chosenPiece.transform.position, positionToMove, moveSpeed * Time.deltaTime);
                yield return null;
            }

            chosenPiece.GetComponent<CreatureToken>().FindTileBellowMe("Move");

            yield return null;
        }
    }

}