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

public class GoogleSpreadSheetManager
{

    void AddData() {

    #region /// 공통영역
    // 샘플에서는 SheetsService.Scope.SpreadsheetsReadonly 으로 되어있는데
    // 데이터를 추가, 수정, 삭제를 하기 위해서는 SheetsService.Scope.Spreadsheets 해준다.
    string[] Scopes = { SheetsService.Scope.Spreadsheets };
    var ApplicationName = "Google Sheets API .NET TEST";
    
    UserCredential credential;
    
    // ClientID를 이용
    using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
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
    
    // GoogleSpreadSheetID 이다.
    var spreadsheetId = "";
    var range = "TestSheet";                     // 시트 이름
    var oblist = new List<object>() { "123456", "kim", "dddddd", "ggg", "sk hynix", "z,.z" };
    var valueRange = new ValueRange() {
    MajorDimension = "ROWS",                    // ROWS or COLUMNS
    Values = new List<IList<object>> { oblist } // 추가할 데이터
    };
    
    var request = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
    request.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;
    request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;
    request.Execute();
    }


}
