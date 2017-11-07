using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    public enum States
    {
        Dead, Alive
    }

    public Material livingMaterial;
    public Material deadMaterial;

    public GameOfLife gameOfLife;
    public int x, y;
    public Cell[] neighbours;
    public States state;
    private States nextState;

    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void CellUpdate()
    {
        nextState = state;
        int aliveCells = GetAliveCells();
        if (state == States.Alive)
        { // if cell is alive
            if (aliveCells != 2 && aliveCells != 3) 
                nextState = States.Dead;
        }
        else
        { 
            if (aliveCells == 3) 
                nextState = States.Alive;
        }
    }
    public void CellApplyUpdate()
    {
        state = nextState;
        UpdateMaterial();
    }

    public void Init(GameOfLife gol, int x, int y)
    {
        gameOfLife = gol;
        transform.parent = gol.transform;

        this.x = x;
        this.y = y;
    }

    // use it to set initial, random cell state
    public void SetRandomState()
    {
        state = (Random.Range(0, 2) == 0) ? States.Dead : States.Alive;
        UpdateMaterial();
    }

    // change cell appearance based on its state
    private void UpdateMaterial()
    {
        if (state == States.Alive)
            meshRenderer.sharedMaterial = livingMaterial;
        else
            meshRenderer.sharedMaterial = deadMaterial;
    }

    // check cell's alive neighbours count
    private int GetAliveCells()
    {
        int ret = 0;
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i] != null && neighbours[i].state == States.Alive)
                ret++;
        }
        return ret;
    }
}

