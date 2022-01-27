
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] _destinations;
    [SerializeField] private GameObject[] _blocks;
    [SerializeField] private float _speed;

    //private Vector2[] _blockNextPosition;
    private int[] _blockIdNextPositions;
    private bool[] _isStraightPath;
    //private bool _isStraightPath;
    private Vector2 _nextPosition;
    private int _idNextPosition;


    // Start is called before the first frame update
    void Start()
    {
        //_timeLeftForDestination = _timeForDestination;
       // _isReturn = false;

       // Vector2 positionFirstBlock = _blocks[0].GetComponent<Transform>().position;
        //float distance = Mathf.Sqrt(Mathf.Pow(positionFirstBlock.x - _endX,2) +  Mathf.Pow(positionFirstBlock.y - _endY,2));
        
        //TODO: Если я ошибочно сделаю различные и иксы, и игреки, то вывести ошибку
        // if(_destination.x == positionFirstBlock.x){
        //     _speed = _timeForDestination / Mathf.Abs(positionFirstBlock.y - _destination.y);
        // } else {
        //     _speed = _timeForDestination / Mathf.Abs(positionFirstBlock.x - _destination.x);
        // }  
        // _nextPosition = _destinations[0].transform.position;
        // _idNextPosition = 0;
        
        _blockIdNextPositions = new int[_destinations.Length];
        for(int i = 0; i < _destinations.Length; i++){
            _blockIdNextPositions[i] = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        
        Debug.Log(_idNextPosition);

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
}
