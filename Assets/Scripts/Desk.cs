using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Desk : MonoBehaviour
{
    public Animation Anim;
    public Material DefaultDrawerMat;
    public Material HighlightDrawerMat;
    public LayerMask DrawerLayerMask;
    public LayerMask PuzzlePieceLayerMask;
    public LayerMask SolutionSlotLayerMask;

    private Ray ray;
    private RaycastHit hit;
    private bool pressingButton = false;
    private bool openingDrawer = false;
    private Collider lastDrawerCollider = null;

    private bool openedDrawer1 = false;
    private bool openedDrawer2 = false;

    public GameObject HeartPuzzlePiecePrefab;
    public GameObject OneSnakePuzzlePiecePrefab;
    public GameObject TwoSnakesPuzzlePiecePrefab;
    public Transform HeartPuzzlePieceHoldingSlot;
    public Transform OneSnakePieceHoldingSlot;
    public Transform TwoSnakesPuzzlePieceHoldingSlot;
    public float PutPuzzlePieceOnTableDurationSecs = 1f;
    private bool draggingPuzzlePiece = false;
    private Collider lastHoveredSolutionSlotCollider = null;

    public List<string> CorrectSequence;
    private List<string> enteredSequence = new List<string>();
    public Pickup PillBottle;


    public PlayerMoveControl mover;

    private void OnEnable()
    {
        PlayerMoveControl player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveControl>();
        if (player.CarryingHeartPiece == true)
        {
            SpawnAndPlacePuzzlePiece(HeartPuzzlePiecePrefab, player.InventoryPointT, HeartPuzzlePieceHoldingSlot);
            player.CarryingHeartPiece = false;
            mover.puzzleUI2.SetActive(false);
        }
        if (player.CarryingOneSnakePiece == true)
        {
            SpawnAndPlacePuzzlePiece(OneSnakePuzzlePiecePrefab, player.InventoryPointT, OneSnakePieceHoldingSlot);
            player.CarryingOneSnakePiece = false;
            mover.puzzleUI3.SetActive(false);
        }
        if (player.CarryingTwoSnakesPiece == true)
        {
            SpawnAndPlacePuzzlePiece(TwoSnakesPuzzlePiecePrefab, player.InventoryPointT, TwoSnakesPuzzlePieceHoldingSlot);
            player.CarryingTwoSnakesPiece = false;
            mover.puzzleUI1.SetActive(false);
        }
    }

    private void SpawnAndPlacePuzzlePiece(GameObject puzzlePiecePrefab, Transform originT, Transform destinationT)
    {
        StartCoroutine(PutPuzzlePieceOnTable(puzzlePiecePrefab, originT, destinationT));
    }

    private IEnumerator PutPuzzlePieceOnTable(GameObject puzzlePiecePrefab, Transform originT, Transform destinationT)
    {
        GameObject newPuzzlePiece = GameObject.Instantiate(puzzlePiecePrefab, originT.position, Quaternion.identity, transform);
        float elapsedTime = 0f;
        while (elapsedTime < PutPuzzlePieceOnTableDurationSecs)
        {
            elapsedTime += Time.fixedDeltaTime;
            newPuzzlePiece.transform.position = Vector3.Lerp(originT.position, destinationT.position, elapsedTime / PutPuzzlePieceOnTableDurationSecs);
            newPuzzlePiece.transform.rotation = Quaternion.Lerp(originT.rotation, destinationT.rotation, elapsedTime / PutPuzzlePieceOnTableDurationSecs);
            yield return null;
        }
        newPuzzlePiece.transform.position = destinationT.position;
        newPuzzlePiece.transform.rotation = destinationT.rotation;
        yield return null;
    }

    private void Update()
    {
        if (!draggingPuzzlePiece && !openingDrawer)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, DrawerLayerMask))
            {
                if (lastDrawerCollider != null && lastDrawerCollider != hit.collider)
                {
                    lastDrawerCollider.GetComponent<Renderer>().material = DefaultDrawerMat;
                    lastDrawerCollider = null;
                }
                if (lastDrawerCollider == null)
                {
                    lastDrawerCollider = hit.collider;
                    lastDrawerCollider.GetComponent<Renderer>().material = HighlightDrawerMat;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    openingDrawer = true;
                    lastDrawerCollider.GetComponent<Renderer>().material = DefaultDrawerMat;
                    if (lastDrawerCollider.gameObject.name == "drawer_1")
                    {
                        if (!openedDrawer1)
                        {
                            Anim.Play("OpenDrawer1");
                            openedDrawer1 = true;
                        }
                        else
                        {
                            Anim.Play("CloseDrawer1");
                            openedDrawer1 = false;
                        }
                    }
                    else
                    {
                        if (!openedDrawer2)
                        {
                            Anim.Play("ShakeDrawer2");
                        }
                        else
                        {
                            openingDrawer = false;
                        }
                    }
                }
            }
            else if (lastDrawerCollider != null)
            {
                lastDrawerCollider.GetComponent<Renderer>().material = DefaultDrawerMat;
                lastDrawerCollider = null;
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, PuzzlePieceLayerMask) && Input.GetMouseButtonDown(0))
            {
                hit.collider.transform.parent.GetComponent<Animation>().Play("Bulge");
                enteredSequence.Add(hit.collider.transform.parent.GetComponent<PuzzlePiece>().PuzzlePieceType);
                if (enteredSequence.Count == 3)
                {
                    if (enteredSequence.SequenceEqual(CorrectSequence))
                    {
                        openedDrawer2 = true;
                        Anim.Play("OpenDrawer2");
                        if (openedDrawer1 == true)
                        {
                            Anim.Play("CloseDrawer1");
                        }
                        GetComponent<Interactable>().LerpCamToOrigPos(true);
                    }
                    else
                    {
                        enteredSequence.RemoveAt(0);
                    }
                }
            }
        }
    }

    public void OnFinishTryingToOpenDrawer()
    {
        openingDrawer = false;
    }

    public void OnFinishOpeningDrawer2()
    {
        PillBottle.Enable();
    }
}
