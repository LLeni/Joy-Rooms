
using System.Data.Common;
using System.Data;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] _destinations;
    [SerializeField] private GameObject[] _blocks;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isPathShowed;

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


        if(_isPathShowed){
            lr = GetComponent<LineRenderer>();  
            lr.material.mainTextureScale = new Vector2(1f / 1, 1.0f);
            DrawPath();
        }

       // GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBlocks();
    }

    private void MoveBlocks(){
        for(int iBlock = 0; iBlock < _blocks.Length; iBlock++){
            GameObject block = _blocks[iBlock];
            Vector2 nextPos = _destinations[_blockIdNextPositions[iBlock]].transform.position;
            block.transform.position = Vector2.MoveTowards(block.transform.position, nextPos, _speed * Time.deltaTime);
            if(block.transform.position.x == nextPos.x && block.transform.position.y == nextPos.y){
                    ChooseBlockNextPosition(iBlock);
            }
        }
    }

    private void ChooseBlockNextPosition(int indexBlock){
        if(_blocks.Length == 1){
            if(_isStraightPath[indexBlock] && _blockIdNextPositions[indexBlock] == _destinations.Length - 1){
                _isStraightPath[indexBlock] = false;
            }
            if(!_isStraightPath[indexBlock] && _blockIdNextPositions[indexBlock] == 0){
                _isStraightPath[indexBlock] = true;
            }
            if(_isStraightPath[indexBlock]){
                _blockIdNextPositions[indexBlock]++;
            } else {
                _blockIdNextPositions[indexBlock]--;
            }
        } else if(indexBlock == 0 && _blockIdNextPositions[indexBlock] == 0 || indexBlock == _blocks.Length - 1 && _blockIdNextPositions[indexBlock] == _destinations.Length - 1){ 

            //Делает один шаг для всех блоков, чтобы не было разрыва
            for (int i = 0; i < _blocks.Length; i++)
            {
                if(i == indexBlock)
                    continue;
               Vector2 nextPos = _destinations[_blockIdNextPositions[i]].transform.position;
               _blocks[i].transform.position = Vector2.MoveTowards(_blocks[i].transform.position, nextPos, _speed * Time.deltaTime);
        
            }

            if(indexBlock == 0 && _blockIdNextPositions[indexBlock] == 0){
                for (int i = 0; i < _isStraightPath.Length; i++)
                {
                    _isStraightPath[i] = true;
                    _blockIdNextPositions[i]++;
                }
            }
            if(indexBlock == _blocks.Length - 1 && _blockIdNextPositions[indexBlock] == _destinations.Length - 1){
                for (int i = 0; i < _isStraightPath.Length; i++)
                {
                    _isStraightPath[i] = false;
                    _blockIdNextPositions[i]--;
                }
            }
        } else {
            if(_isStraightPath[indexBlock]){
                _blockIdNextPositions[indexBlock]++;
            } else {
                _blockIdNextPositions[indexBlock]--;
            }
        }
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
