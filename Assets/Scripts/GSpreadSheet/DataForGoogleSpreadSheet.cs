using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Student Data", menuName ="Scriptable Object/Student Data", order = int.MaxValue)]
public class DataForGoogleSpreadSheet : ScriptableObject
{   
    //학번
    [SerializeField]
    private int studentId;
    public int StudentId {get {return studentId;}}

    //이름
    [SerializeField]
    private string studentName;
    public string StudentName{get{return studentName;}}

    //코인 갯수
    [SerializeField]
    private int coinCount;
    public int CoinCount{get{ return coinCount;}}

    //메달 갯수
    [SerializeField]
    private int medalCount;
    public int MedalCount{get{return medalCount;}}

    //등록 일시
    [SerializeField]
    private DateTime registeredDateAndTime;
    public DateTime RegisteredDateAndTime{get{return registeredDateAndTime;}}
}
