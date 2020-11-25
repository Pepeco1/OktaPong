using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private EndGameCanvasController endGameUI = null;

    private List<PieceAbstract> player1Pieces = new List<PieceAbstract>();
    private List<PieceAbstract> player2Pieces = new List<PieceAbstract>();

    private int tieCount = 0;

    public void RegisterPiece(PieceAbstract piece)
    {
        if (piece.filiation == PieceFiliation.player1)
            player1Pieces.Add(piece);
        if (piece.filiation == PieceFiliation.player2)
            player2Pieces.Add(piece);
    }

    public void UnregisterPiece(PieceAbstract piece)
    {
        if (piece.filiation == PieceFiliation.player1)
            player1Pieces.Remove(piece);
        if (piece.filiation == PieceFiliation.player2)
            player2Pieces.Remove(piece);

        CheckForEndGameConditions();
    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnTurnoChange()
    {
        if (player1Pieces.Count <= 2 && player2Pieces.Count <= 2)
        {
            tieCount += 1;
            // Se está nos parametros de empate
            if (tieCount == 5)
                TieGame();
        }
    }

    private void CheckForEndGameConditions()
    {
        if (player1Pieces.Count == 0 || player2Pieces.Count == 0)
            EndGame();

        //Piece was eaten -> reset tie count
        tieCount = -1;

    }

    private void EndGame()
    {
        if(player1Pieces.Count > player2Pieces.Count)
        {
            endGameUI.SetText("Player1 venceu");
        }
        else
        {
            endGameUI.SetText("Player2 venceu");
        }

        CanvasManager.Instance.SwitchState(CanvasType.EndGame);
    }

    private void TieGame()
    {
        endGameUI.SetText("Empate!");
        CanvasManager.Instance.SwitchState(CanvasType.EndGame);
    }

}
