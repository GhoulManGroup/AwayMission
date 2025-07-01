using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Navigation;

public class GridSpace : MonoBehaviour
{

    #region Declerations
    [Header("")]

    float xPosition;
    float yPosition;

    public string myState = "Empty";

    public List<GameObject> Neighbours = new List<GameObject>();

    Pathfinder pathfinder;

    #endregion

    #region SetupGridSpace

    void Start()
    {
        xPosition = this.transform.position.x;
        yPosition = this.transform.position.y;
        DeclareNeighbours();
    }

    public void DeclareNeighbours()
    {
        Neighbours.Clear();

        RaycastHit Forward;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Forward, 1f))
        {
            if (Forward.collider != null)
            {
                if (Forward.collider.GetComponent<GridSpace>() != null)
                {
                    Neighbours.Add(Forward.collider.gameObject);
                }
            }
        }

        RaycastHit Back;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out Back, 1f))
        {
            if (Back.collider != null)
            {
                if (Back.collider.GetComponent<GridSpace>() != null)
                {
                    Neighbours.Add(Back.collider.gameObject);
                }
            }
        }

        RaycastHit Left;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out Left, 1f))
        {
            if (Left.collider != null)
            {
                if (Left.collider.GetComponent<GridSpace>() != null)
                {
                    Neighbours.Add(Left.collider.gameObject);
                }
            }
        }

        RaycastHit Right;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out Right, 1f))
        {
            if (Right.collider != null)
            {
                if (Right.collider.GetComponent<GridSpace>() != null)
                {
                    Neighbours.Add(Right.collider.gameObject);
                }
            }
        }


        RaycastHit ForwardRight;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + Vector3.right), out ForwardRight, 1f))
        {
            if (ForwardRight.collider != null)
            {
                if (ForwardRight.collider.GetComponent<GridSpace>() != null)
                {
                    Neighbours.Add(ForwardRight.collider.gameObject);
                }
            }
        }

        RaycastHit DownRight;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back + Vector3.right), out DownRight, 1f))
        {
            if (DownRight.collider != null)
            {
                if (DownRight.collider.GetComponent<GridSpace>() != null)
                {
                    Neighbours.Add(DownRight.collider.gameObject);
                }
            }
        }

        RaycastHit DownLeft;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back + Vector3.left), out DownLeft, 1f))
        {
            if (DownLeft.collider != null)
            {
                if (DownLeft.collider.GetComponent<GridSpace>() != null)
                {
                    Neighbours.Add(DownLeft.collider.gameObject);
                }
            }
        }

        RaycastHit ForwardLeft;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + Vector3.left), out ForwardLeft, 1f))
        {
            if (ForwardLeft.collider != null)
            {
                if (ForwardLeft.collider.GetComponent<GridSpace>() != null)
                {
                    Neighbours.Add(ForwardLeft.collider.gameObject);
                }
            }
        }
    }

    #endregion

    #region PlayerCreatureMovementCode
    /*
    public void FindPossibleMovements()
    {
        // We know how many tiles from start pos we could move now we check if there is anywhere we can move.
        for (int i = 0; i < Neighbours.Count; i++)
        {
            if (pathfinder.tilesToCheck.Contains(Neighbours[i].gameObject) || pathfinder.checkedTiles.Contains(Neighbours[i].gameObject))
            {
                //Already Checked don't do anything more with it.
            }
            else
            {
                int Dist = distanceFromStartTile + 1;
                // Add 1 to distance from  its current distance from start tile value then if that value is within our possible move distance that neighbour is in reach of our board piece.
                if (Neighbours[i].GetComponent<GridScript>().distanceFromStartTile + Dist <= LvlRef.GetComponent<PathController>().possibleMoveDistance)
                {//The above statement can't be more than 1 beacuse when it is checked it will always be 0
                    //Check then if that tile is a dungeon tile and is currently not containing any other board piece.
                    if (Neighbours[i].GetComponent<GridScript>().myState == "DungeonTile" && Neighbours[i].GetComponent<GridScript>().TileContents == "Empty")
                    {
                        //Then if all these conditions are met we add the tile to the list to check and increase that tiles distancefromstart by 1 space to indicate its 1 away from this tile.
                        LvlRef.GetComponent<PathController>().tilesToCheck.Add(Neighbours[i].gameObject);
                        Neighbours[i].gameObject.GetComponent<GridScript>().distanceFromStartTile = distanceFromStartTile + 1;

                        //Add the neighbour to the list of possible tiles to move to.
                        LvlRef.GetComponent<PathController>().reachableTiles.Add(Neighbours[i].gameObject);
                    }
                }
            }
        }
        //Remove this checked tile from the list of tiles to check add it to the checked list.
        pathfinder.tilesToCheck.Remove(this.gameObject);
        pathfinder.checkedTiles.Add(this.gameObject);
        pathfinder.EstablishPossibleMoves("CheckPossibleMoves");
    }

    public void ShowPossibleMovements()
    {
        this.SetIndicatorMaterial("MoveSpace");
        updateTMPRO();
    }

    public void FindPossiblePathToStart()
    {
        //This list exists to pick a path if there are branching valid choices back.
        List<GameObject> dupliacteProtect = new List<GameObject>();

        WhereInFindPathIsBug = "Neighbour Check Loop";

        for (int i = 0; i < Neighbours.Count; i++)
        {//If the current one isn't start then check if its one of the grid tiles we already established as a valid move then check its closer to start than we already are.    
            if (pathfinder.tilesToCheck.Contains(Neighbours[i].gameObject))
            {
                pathfinder.tilesToCheck.Remove(this.gameObject);
                pathfinder.EstablishPossibleMoves("FindPath");
            }
            else
            {
                if (Neighbours[i] != pathController.startPosition)
                {
                    if (pathfinder.reachableTiles.Contains(Neighbours[i]))
                    {
                        if (Neighbours[i].GetComponent<GridScript>().distanceFromStartTile == distanceFromStartTile - 1)
                        {//Then encase there are multiple vaild routes back to start we add it to duplicates so we can randomly pick one of those neighbour tiles once we found them all.
                            dupliacteProtect.Add(Neighbours[i]);
                        }
                    }
                }//Else if it is start we found our intended move spot so stop the function.
                else if (Neighbours[i] == pathfinder.startPosition)
                {
                    //Debug.LogError("Found Start" + Neighbours[i].gameObject.name);
                    WhereInFindPathIsBug = "Found Start";
                    pathfinder.tilesToCheck.Remove(this.gameObject);
                    pathfinder.EstablishPossibleMoves("FindPath");
                    return;
                }
            }

        }

        while (dupliacteProtect.Count > 1)
        {
            WhereInFindPathIsBug = "Remove Duplicate Possible Path";
            int removeMe = Random.Range(0, dupliacteProtect.Count);
            dupliacteProtect.RemoveAt(removeMe);
        }

        if (dupliacteProtect.Count == 1)
        {
            WhereInFindPathIsBug = "Tile Chosen Return To Find Path";
            pathfinder.chosenPathTiles.Add(dupliacteProtect[0]);
            pathfinder.tilesToCheck.Add(dupliacteProtect[0]);
            pathfinder.tilesToCheck.Remove(this.gameObject);
            pathfinder.EstablishPossibleMoves("FindPath");
        }

    }

    public void MoveCreaturetome()
    {// Use for current quick (Instant move) only. // Might use for abilities which instalty teleport so keep in during rewrite
        if (pathfinder.quickMove == true)
        {
            pathfinder.ChosenCreatureToken.transform.position = new Vector3(this.transform.position.x, 0.3f, this.transform.position.z);
            pathfinder.ChosenCreatureToken.GetComponent<CreatureToken>().FindTileBellowMe("Move");
            //LvlRef.GetComponent<LevelController>().participants[LvlRef.GetComponent<LevelController>().currentTurnParticipant].GetComponent<Player>().moveCrestPoints -= distanceFromStartTile;            
            TileContents = "Creature";
            pathfinder.HasMoved();
        }
    }
 */       
    #endregion
        
}