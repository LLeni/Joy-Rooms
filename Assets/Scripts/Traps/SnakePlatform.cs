﻿using System.Data;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] _destinations;
    [SerializeField] private GameObject[] _blocks;
    [SerializeField] private float _speed;

    //private Vector2[] _blockNextPosition;
    private int[] _blockIdNextPositions;
    private bool[] _isStraightPath;
    //private bool _isStraightPath;


    private LineRenderer lr;
    


    // Start is called before the first frame update
    void Start()
    {
        _blockIdNextPositions = new int[_destinations.Length];
        _isStraightPath = new bool[_destinations.Length];
        for(int i = 0; i < _destinations.Length; i++){
            _blockIdNextPositions[i] = 1;
            _isStraightPath[i] = true;
        }

        lr = GetComponent<LineRenderer>();  
        lr.material.mainTextureScale = new Vector2(1f / 1, 1.0f);
        DrawPath();

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    // Update is called once per frame
    void Update()
    {

        MoveBlocks();
        for(int iBlock = 0; iBlock < _blocks.Length; iBlock++){
            GameObject block = _blocks[iBlock];
            Vector2 nextPos = _destinations[_blockIdNextPositions[iBlock]].transform.position;
            block.transform.position = Vector2.MoveTowards(block.transform.position, nextPos, _speed * Time.deltaTime);
            if(iBlock == 0 || iBlock == _blocks.Length - 1){
                if(block.transform.position.x == nextPos.x && block.transform.position.y == nextPos.y){
                    ChooseBlockNextPosition(iBlock);
                }
            } else {
                //С учетом позиции в "змее"
                if(block.transform.position.x == nextPos.x - iBlock && block.transform.position.y == nextPos.y - iBlock){
                    ChooseBlockNextPosition(iBlock);
                }
            }

            //if(((_blocks[0].transform.position.x == _nextPosition.x) && (_blocks[0].transform.position.y == _nextPosition.y)) || ((_blocks[_blocks.Length-1].transform.position.x == _nextPosition.x) && (_blocks[_blocks.Length-1].transform.position.y == _nextPosition.y))){
        }
        
        //Debug.Log(_blockIdNextPositions[0]);

    }

    private void MoveBlocks(){
        for(int iBlock = 0; iBlock < _blocks.Length; iBlock++){
            GameObject block = _blocks[iBlock];
            Vector2 nextPos = _destinations[_blockIdNextPositions[iBlock]].transform.position;
            block.transform.position = Vector2.MoveTowards(block.transform.position, nextPos, _speed * Time.deltaTime);
            if(iBlock == 0 || iBlock == _blocks.Length - 1){
                if(block.transform.position.x == nextPos.x && block.transform.position.y == nextPos.y){
                    ChooseBlockNextPosition(iBlock);
                }
            } else {
                //С учетом позиции в "змее"
                if(block.transform.position.x == nextPos.x - iBlock && block.transform.position.y == nextPos.y - iBlock){
                    ChooseBlockNextPosition(iBlock);
                }
            }

            //if(((_blocks[0].transform.position.x == _nextPosition.x) && (_blocks[0].transform.position.y == _nextPosition.y)) || ((_blocks[_blocks.Length-1].transform.position.x == _nextPosition.x) && (_blocks[_blocks.Length-1].transform.position.y == _nextPosition.y))){
        }
    }

    private void ChooseBlockNextPosition(int indexBlock){
        //GameObject block = _blocks[indexBlock];
        if(indexBlock == 0 || indexBlock == _blocks.Length - 1){
                if(_blockIdNextPositions[indexBlock] == _destinations.Length - 1){
                    _isStraightPath[indexBlock] = false;
                }
                if(_blockIdNextPositions[indexBlock] == 0){
                    _isStraightPath[indexBlock] = true;
                }        
        } else {
                if(_blockIdNextPositions[indexBlock] == _destinations.Length - 1 - indexBlock){
                    _isStraightPath[indexBlock] = false;
                }
                if(_blockIdNextPositions[indexBlock] == 0 + indexBlock){
                    _isStraightPath[indexBlock] = true;
                }
        }

        if(_isStraightPath[indexBlock]){
            _blockIdNextPositions[indexBlock]++;
        } else {
            _blockIdNextPositions[indexBlock]--;
        }
        //return _destinations[_idNextPosition].transform.position;
        
    }

    private void DrawPath(){
        lr.positionCount = _destinations.Length;
        for(int i = 0; i < _destinations.Length; i++){
            lr.SetPosition(i, _destinations[i].transform.position);
        }
    }

    private void OnGameStateChanged(GameState newGameState){
        enabled = newGameState == GameState.Gameplay;
    }
    
}