using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Newtonsoft.Json;
using System;
using Google.Apis.Sheets.v4.Data;
using System.IO;
using Google.Apis.Util.Store;
using System.Threading;
using PixelCrushers.DialogueSystem;

public class GoogleSpreadSheetManager : MonoBehaviour
{

    
    [Tooltip("Typically leave unticked so temporary Dialogue Managers don't unregister your functions.")]
    public bool unregisterOnDisable = false;

    #region /// Google Spreadsheet 공통영역
    // 샘플에서는 SheetsService.Scope.SpreadsheetsReadonly 으로 되어있는데
    // 데이터를 추가, 수정, 삭제를 하기 위해서는 SheetsService.Scope.Spreadsheets 해준다.
    static string[] Scopes = { SheetsService.Scope.Spreadsheets };
    static string ApplicationName = "MetaKaywon";
    
    void AddData(double dummy) {

    UserCredential credential;
    
    // ClientID를 이용
    using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
    {
    string credPath = "token.json";
    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        GoogleClientSecrets.Load(stream).Secrets,
        Scopes,
        "user",
        CancellationToken.None,
        new FileDataStore(credPath, true)).Result;
    }
    
    // API 서비스 생성
    var service = new SheetsService(new BaseClientService.Initializer()
    {
    HttpClientInitializer = credential,
    ApplicationName = ApplicationName,
    }); 
    #endregion
    
    // GoogleSpreadSheetID 이다. URL에서 확인 가능
    var spreadsheetId = "1eD1lEe9TgxWXTTITfq9J4H7cpnTwgE_3RtyWf1YtwmQ";
    var range = "A2";                     // 시트 이름  ->  안먹힘. 범위로 설정해야함.

    //   SpreadsheetsResource.ValuesResource.GetRequest request =
    //                 service.Spreadsheets.Values.Get(spreadsheetId, range);

    //   ValueRange response =  request.Execute(); 
    //   IList<IList<System.Object>> values = response.Values;
      
    //   var oblist = new List<object>() { "20xx123456", "김형진", "30", "8", "230204 22:11:10"};

    //   values.Add(oblist);

    var oblist = new List<object>() { "20xx123456", "김형진", "30", "8", "230204 22:11:10"};
    var valueRange = new ValueRange() {
    MajorDimension = "ROWS",                    // ROWS or COLUMNS
    Values = new List<IList<object>> { oblist } // 추가할 데이터

    };

    var request =
                     service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
    request.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;
    request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;
    request.Execute();
    }



    
    #region Register with Lua
    void OnEnable()
    {
        // Make the functions available to Lua: (Replace these lines with your own.)
        // Lua.RegisterFunction("DebugLog", this, SymbolExtensions.GetMethodInfo(() => DebugLog(string.Empty)));
        // Lua.RegisterFunction("AddOne", this, SymbolExtensions.GetMethodInfo(() => AddOne((double)0)));

        Lua.RegisterFunction("AddData", this, SymbolExtensions.GetMethodInfo(() => AddData((double)0)));
    }

    void OnDisable()
    {
        if (unregisterOnDisable)
        {
            // Remove the functions from Lua: (Replace these lines with your own.)
            // Lua.UnregisterFunction("DebugLog");
            // Lua.UnregisterFunction("AddOne");
            Lua.UnregisterFunction("AddData");

        }
    }

   #endregion

}
