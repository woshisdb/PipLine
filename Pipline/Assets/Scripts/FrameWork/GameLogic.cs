using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Michsky.MUIP;
using PimDeWitte.UnityMainThreadDispatcher;
using QFramework;
using Sirenix.OdinInspector;
using System.Text.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Newtonsoft.Json;
using MathNet.Numerics.Statistics;
/// <summary>
/// 更新环境
/// </summary>
public class EnvirUpdateEvent
{

}
/// <summary>
/// 开始选择活动
/// </summary>
public class BeginSelectEvent
{

}
public class EndSelectEvent
{
}
/// <summary>
/// 描述今天的工作
/// </summary>
public class CodeSystemData
{
    /// <summary>
    /// 每一天的工作
    /// </summary>
    public int nowTime;
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public List<List<CodeData>> work;
    /// <summary>
    /// 获取当前的工作列表
    /// </summary>
    /// <returns></returns>
    public List<CodeData> GetNowWorks()
    {
        return work[nowTime];
    }
    public void NextDay()
    {
        nowTime=(nowTime+1)%work.Count;
    }
    public CodeSystemData()
    {
        work= new List<List<CodeData>>();
        nowTime=0;
    }
    //public virtual IEnumerator EditCodeSystem(PersonObj PersonObj, Obj obj,CodeData codeData)
    //{
    //    List<Activity> activities = obj.InitActivities();
    //    List<CardInf> sels = new List<CardInf>();
    //    Activity activity = null;
    //    foreach (var x in activities)
    //    {
    //        var data = x;
    //        sels.Add(new CardInf("选择:" + data.activityName, data.detail,
    //            () =>
    //            {
    //                activity = data;
    //            }
    //        ));
    //    }

    //    yield return GameArchitect.gameLogic.AddDecision(PersonObj, new DecisionTex(
    //    "工作的内容", "选择工作的内容进行工作", sels));
    //    foreach (var x in work)
    //    {
    //        x.obj = obj;
    //    }
    //    /////////////////////////////////////////////////////////////////
    //    yield return GetActDec(codeData, obj, activity);
    //}
    //public IEnumerator GetActDec(CodeData codeData, Obj obj,Activity activity)
    //{
    //    var w= codeData;
    //    PersonObj tempPersonObj = new PersonObj();//自建PersonObj
    //    BuildingObj buildingObj = new BuildingObj();
    //    w.activity = activity;
    //    List<WinData> winDatas = new List<WinData>();
    //    var eff = activity.Effect(obj, tempPersonObj, winDatas);
    //    tempPersonObj.SetAct(eff);
    //    tempPersonObj.isPlayer = true;
    //    Debug.Log(eff.GetType().Name);
    //    bool hasTime = GameLogic.hasTime;
    //    while (tempPersonObj.hasSelect == true)
    //    {
    //        GameLogic.hasTime = true;
    //        yield return tempPersonObj.act.Run(
    //            (result) => {
    //                if (result is EndAct)
    //                    tempPersonObj.RemoveAct();
    //                else if (result is Act)
    //                    tempPersonObj.SetAct((Act)result);
    //            }
    //        );
    //    }
    //    GameLogic.hasTime = hasTime;
    //    ///选择一系列活动
    //    w.wins = winDatas;
    //}
}
//public class CodeSystemDataWeek: CodeSystemData
//{
//    public CodeSystemDataWeek():base()
//    {
//        code = CodeSystemEnum.week;
//        week = new List<int>(7);
//        for(int i = 0; i < 7; i++)
//        { week.Add(0); }
//    }
//    public override IEnumerator EditCodeSystem(PersonObj PersonObj,Obj obj, CodeData codeData)
//    {
//        List<Activity> activities = obj.InitActivities();
//        List<CardInf> sels = new List<CardInf>();
//        Activity activity = null;
//        foreach(var x in activities)
//        {
//            var data = x;
//            sels.Add(new CardInf("选择:"+data.activityName,data.detail,
//                () =>
//                {
//                    activity = data;
//                }
//            ));
//        }

//        yield return GameArchitect.gameLogic.AddDecision(PersonObj,new DecisionTex(
//        "工作的内容","选择工作的内容进行工作",sels));
//        foreach(var x in work)
//        {
//            x.obj = obj;
//        }
//        /////////////////////////////////////////////////////////////////
//        yield return GetActDec(codeData, obj,activity);
//    }
//}

//public class CodeSystemDataMove : CodeSystemData
//{
//    public CodeSystemDataMove() : base()
//    {
//        code = CodeSystemEnum.week;
//        week = new List<int>(7);
//        for (int i = 0; i < 7; i++)
//        { week.Add(0); }
//    }
//    public override IEnumerator EditCodeSystem(PersonObj PersonObj, Obj obj, CodeData codeData)
//    {
//        List<Activity> activities = obj.InitActivities();
//        List<CardInf> sels = new List<CardInf>();
//        Activity activity = null;
//        foreach (var x in activities)
//        {
//            var data = x;
//            sels.Add(new CardInf("选择:" + data.activityName, data.detail,
//                () =>
//                {
//                    activity = data;
//                }
//            ));
//        }

//        yield return GameArchitect.gameLogic.AddDecision(PersonObj, new DecisionTex(
//        "工作的内容", "选择工作的内容进行工作", sels));
//        foreach (var x in work)
//        {
//            x.obj = obj;
//        }
//        var w = work.Find((x) => { return x.codeName == "搬运"; });
//        PersonObj tempPersonObj = new PersonObj();//自建PersonObj
//        BuildingObj buildingObj = new BuildingObj();
//        w.activity = activity;
//        List<WinData> winDatas = new List<WinData>();
//        var eff = activity.Effect(obj, tempPersonObj, winDatas);
//        tempPersonObj.SetAct(eff);
//        tempPersonObj.isPlayer = true;
//        Debug.Log(eff.GetType().Name);
//        bool hasTime = GameLogic.hasTime;
//        while (tempPersonObj.hasSelect == true)
//        {
//            GameLogic.hasTime = true;
//            yield return tempPersonObj.act.Run(
//                (result) => {
//                    if (result is EndAct)
//                        tempPersonObj.RemoveAct();
//                    else if (result is Act)
//                        tempPersonObj.SetAct((Act)result);
//                }
//            );
//        }
//        GameLogic.hasTime = hasTime;
//        ///选择一系列活动
//        w.wins = winDatas;
//    }
//}



/// <summary>
/// 选择行为
/// </summary>
public class WinData
{
    
}

public class SelData:WinData
{
    public List<System.Tuple<string, int>> selects;//选择的行为
    public SelData()
    {
        selects = new List<System.Tuple<string, int>>();
    }
}

public class DecData : WinData
{
    public string selc;
}

public struct BeginActEvent
{
    public PersonObj PersonObj;
    public BeginActEvent(PersonObj PersonObj)
    {
        this.PersonObj = PersonObj;
    }
}
public struct EndActEvent
{
    public PersonObj PersonObj;
    public EndActEvent(PersonObj PersonObj)
    {
        this.PersonObj = PersonObj;
    }
}
public class SelectCardEvent
{
    public CardInf card;
    public PersonObj PersonObj;
    public SelectCardEvent(CardInf card, PersonObj PersonObj)
    {
        this.card = card;
        this.PersonObj = PersonObj;
    }
}
public class GameLogic : MonoBehaviour,ICanRegisterEvent
{
    public static OptionUIEnum optionUIEnum;
    public static bool isCoding=false;
    //相机
    public Camera camera;
    public static WindowManager windowsmanager;
    public static List<UnityAction> unityActions;
    protected TaskCompletionSource<bool> tcs;
    public static bool hasTime;
    public CodeControler codeControler;
    // Start is called before the first frame update
    public IEnumerator WindowSwitch()
    {
        while (true)
        {
            if (unityActions.Count > 0)
            {
                unityActions[0].Invoke();
                unityActions.RemoveAt(0);
                yield return null;
            }
            else
            {
                yield return null;
            }
        }
    }
    public void Awake()
    {
        //codeControler = GameObject.Find("CodeControler").GetComponent<CodeControler>();
        windowsmanager = GameObject.Find("Window Manager").GetComponent<WindowManager>();
        windowsmanager.onWindowChange.AddListener(e=> { OnWinChange(e); });
        unityActions = new List<UnityAction>();
        StartCoroutine(WindowSwitch());
    }
    [Button]
    public void Refresh()
    {
        var cnodes=PDDLClassGenerater.Refresh();
        foreach(var x in cnodes)
        {
            PDDLUIMenu.PDDLGen(x);
        }

    }
    public void OnWinChange(int win)//待修改
    {
        //Debug.Log(win);
        if (win == 2)
        {
            GameLogic.isCoding = true;
            //-----------------------GameArchitect.gameLogic.codeControler.InitObjCards();
        }
        else
        {
            //GameArchitect.gameLogic.codeControler.nowObj = null;
            //GameArchitect.gameLogic.codeControler.nowActivity = null;
            //GameArchitect.gameLogic.codeControler.nowAct.text = "";
            GameLogic.isCoding = false;
        }
    }
    public static void ToNoSelect()
    {
        GameLogic.optionUIEnum = OptionUIEnum.NoSelect;
        unityActions.Add(()=> { windowsmanager.OpenWindowByIndex(1); });
    }
    public static void ToSelect()
    {
        GameLogic.optionUIEnum = OptionUIEnum.Select;
        unityActions.Add(() => { windowsmanager.OpenWindowByIndex(0); });
    }
    public static void ToCoding()
    {
        GameLogic.isCoding = true;
        //----------------------GameArchitect.gameLogic.codeControler.InitObjCards();
        unityActions.Add(() => {
        windowsmanager.OpenWindowByIndex(2);
        });
    }
    public static void NotCoding()
    {
        GameLogic.isCoding = false;
        unityActions.Add(() => {
            windowsmanager.OpenWindowByIndex(0);
        });
    }
    public IArchitecture GetArchitecture()
    {
        return GameArchitect.Interface;
    }

    public void Start()
    {
        this.RegisterEvent<EnvirUpdateEvent>(e=> { UpdateEnvir(e); });
        this.RegisterEvent<BeginSelectEvent>(e=> { BeginSelectEvent(e); });
        this.RegisterEvent<EndActEvent>(
            e =>
            {
                tcs.TrySetResult(true);
            }
        );
        this.SendEvent<EnvirUpdateEvent>();//开始执行
        if (GameArchitect.get.player != null)
        {
            camera.transform.position = GameArchitect.get.player.belong.control.CenterPos();
        }
    }
    //更新环境信息
    public void UpdateEnvir(EnvirUpdateEvent envirUpdateEvent)
    {
        MainDispatch.Instance().Enqueue(
            () => {
                //Debug.Log(245);
                foreach (var t in GameArchitect.get.tableAsset.tableSaver.objs)//选择后更新
                {
                    t.LatUpdate();
                }
                GameArchitect.Interface.GetModel<TimeModel>().AddTime();
                foreach (var t in GameArchitect.get.tableAsset.tableSaver.objs)//选择前更新
                {
                    t.BefUpdate();
                }
                this.SendEvent<BeginSelectEvent>(new BeginSelectEvent());
                this.SendEvent<UpdateCode>(new UpdateCode(GameArchitect.get.player));
            }
        );
    }
    public IEnumerator AddDecision(PersonObj PersonObj,WinCon decision)
    {
        if (PersonObj.codeData != null && PersonObj.codeData.wins != null && PersonObj.codeData.wins.Count > 0)
        {
            decision.Decision(PersonObj.codeData.wins[0]);
            PersonObj.codeData.wins.RemoveAt(0);
        }
        else
        {
            Debug.Log(PersonObj.name + "," + decision.ToString());
            if (PersonObj.isPlayer)
            {
                yield return GameArchitect.get.AddDecision(decision);
            }
            else//调用游戏的决策系统
            {
                //yield return GameArchitect.decisionUI.AddDecision(decision.title, decision.description, decision.cards);
            }
        }
    }
    [Button]
    public void AddObj(ObjEnum objEnum,string SceneName,ObjSaver objSaver=null)
    {
        var type = Map.Instance.enum2Type[objEnum];
        var val = (Obj)Activator.CreateInstance(type, new object[] { objSaver });
        var Sc = ((GameArchitect)GameArchitect.Interface).tableAsset.tableSaver.tables.Find(e => { return e.TableName == SceneName; });
        if (Sc == null)
        {
            Debug.Log("无法创建");
            return;
        }
        Sc.AddToTable(val);
    }
    [Button]
    public void GetPDDLAsync()
    {
		var ret = Map.InitPDDL();
        string domainStr = ret.Item1.Print();
        string problemStr = ret.Item2.Print();
  //      Debug.Log(ret.Item1.Print());
		////File.WriteAllText($"Assets/Resources/PDDL/Domain.txt", ret.Item1.Print());
		//Debug.Log(ret.Item2.Print());
		//File.WriteAllText($"Assets/Resources/PDDL/Problem.txt", ret.Item2.Print());
		//AssetDatabase.Refresh();
		string url = "http://localhost:8080/run";
        //StartCoroutine(GetRequest(url));
        SendPostRequestAsync(url,domainStr,problemStr);
        //Debug.Log("Response Content: " + response);
        //Expression<Func<PersonObj, int>> func = (a) => a.resource.maxSize;
        ////TraverseExpression(func);
        //TraverseExpression(func.Body);
    }
    [System.Serializable]
    private class ActionListWrapper
    {
        public List<List<string>> actions;
    }
    public static List<List<string>> ParseJsonString(string jsonString)
    {
        // 使用JsonConvert.DeserializeObject将JSON字符串解析为List<List<string>>
        List<List<string>> parsedData = JsonConvert.DeserializeObject<List<List<string>>>(jsonString);
        return parsedData;
    }
    public static async Task<HttpResponseMessage> SendPostRequestAsync(string url, string domainStr, string problemStr)
    {
        using (HttpClient client = new HttpClient())
        {
			//string domainFilePath = Path.Combine(Application.dataPath, "domain.pddl");
			//string problemFilePath = Path.Combine(Application.dataPath, "problem.pddl");

			//// 读取文件内容
			//string domainContent = File.ReadAllText(domainFilePath);
			//Debug.Log(domainContent);
			//string problemContent = File.ReadAllText(problemFilePath);
			//Debug.Log(problemContent);

			string domainContent = domainStr;
			string problemContent = problemStr;

			var data = new MultipartFormDataContent();
            data.Add(new StringContent(domainContent), "x");
            data.Add(new StringContent(problemContent),"y");
            HttpResponseMessage response = await client.PostAsync(url, data);
            if (response.IsSuccessStatusCode)
            {
                // 获取响应内容
                string responseBody = await response.Content.ReadAsStringAsync();
                Debug.Log(responseBody);
                var result= ParseJsonString(responseBody);
                Debug.Log(result.Count);
			}
            return response;
        }
    }
    //IEnumerator GetRequest(string url)
    //{
    //    // 定义文件路径
    //    string domainFilePath = Path.Combine(Application.dataPath, "domain.pddl");
    //    string problemFilePath = Path.Combine(Application.dataPath, "problem.pddl");

    //    // 读取文件内容
    //    string domainContent = File.ReadAllText(domainFilePath);
    //    string problemContent = File.ReadAllText(problemFilePath);

    //    // 创建一个表单数据，类似于MultipartFormDataContent
    //    WWWForm form = new WWWForm();
    //    form.AddField("x", domainContent);
    //    form.AddField("y", problemContent);

    //    // 使用 UnityWebRequest 发送POST请求
    //    UnityWebRequest www = UnityWebRequest.Post(url, form);

    //    // 发送请求并等待响应
    //    yield return www.SendWebRequest();

    //    if (www.result != UnityWebRequest.Result.Success)
    //    {
    //        Debug.LogError("POST请求失败: " + www.error);
    //    }
    //    else
    //    {
    //        Debug.Log(www.downloadHandler.text);
    //        List<List<string>> result = ParseJsonString(www.downloadHandler.text);
    //        // 打印解析后的数据
    //    if (result != null)
    //    {
    //        foreach (var itemList in result)
    //        {
    //            Debug.Log(string.Join(", ", itemList));
    //        }
    //    }
    //        //Debug.Log("POST请求成功，服务器响应: " + www.downloadHandler.text);
    //    }
    //}
    //private static void TraverseExpression(Expression expr)
    //{
    //    if (expr.NodeType == ExpressionType.Lambda)
    //    {
    //        var lambda = (LambdaExpression)expr;
    //        Debug.Log("Lambda: " + lambda);
    //        TraverseExpression(lambda.Body);
    //        foreach (var param in lambda.Parameters)
    //        {
    //            Debug.Log("Parameter: " + param.Name + " Type: " + param.Type);
    //        }
    //    }
    //    else if (expr.NodeType == ExpressionType.GreaterThan ||
    //             expr.NodeType == ExpressionType.LessThan ||
    //             expr.NodeType == ExpressionType.Equal ||
    //             expr.NodeType == ExpressionType.NotEqual)
    //    {
    //        var binaryExpr = (BinaryExpression)expr;
    //        Debug.Log("Binary Expression: " + binaryExpr.NodeType);
    //        TraverseExpression(binaryExpr.Left);
    //        TraverseExpression(binaryExpr.Right);
    //    }
    //    else if (expr.NodeType == ExpressionType.Parameter)
    //    {
    //        var paramExpr = (ParameterExpression)expr;
    //        Debug.Log("Parameter: " + paramExpr.Name + " Type: " + paramExpr.Type);
    //    }
    //    else if (expr.NodeType == ExpressionType.Constant)
    //    {
    //        var constExpr = (ConstantExpression)expr;
    //        Debug.Log("Constant: " + constExpr.Value + " Type: " + constExpr.Type);
    //    }
    //    else if (expr.NodeType == ExpressionType.MemberAccess)
    //    {
    //        var memberExpr = (MemberExpression)expr;
    //        Debug.Log("Member Access: " + memberExpr.Member.Name);
    //        TraverseExpression(memberExpr.Expression);
    //    }
    //    else if (expr.NodeType == ExpressionType.Call)
    //    {
    //        var callExpr = (MethodCallExpression)expr;
    //        Debug.Log("Method Call: " + callExpr.Method.Name);
    //        foreach (var arg in callExpr.Arguments)
    //        {
    //            TraverseExpression(arg);
    //        }
    //        TraverseExpression(callExpr.Object);
    //    }
    //    else
    //    {
    //        Debug.Log("Unhandled expression type: " + expr.NodeType);
    //    }
    //}
    private static Pop TraverseExpression(Expression expr)
    {
        Pop ret = null;
        if (expr.NodeType == ExpressionType.Lambda)
        {
            var lambda = (LambdaExpression)expr;
            Debug.Log("Lambda: " + lambda);
            TraverseExpression(lambda.Body);
            foreach (var param in lambda.Parameters)
            {
                Debug.Log("Parameter: " + param.Name + " Type: " + param.Type);
            }
        }
        else if (expr.NodeType == ExpressionType.GreaterThan)
        {
            var binaryExpr = (BinaryExpression)expr;
            Debug.Log("Binary Expression: " + binaryExpr.NodeType);
            ret = new Great(TraverseExpression(binaryExpr.Left), TraverseExpression(binaryExpr.Right));
        }
        else if (expr.NodeType == ExpressionType.GreaterThanOrEqual)
        {
            var binaryExpr = (BinaryExpression)expr;
            Debug.Log("Binary Expression: " + binaryExpr.NodeType);
            ret = new GreatEqual(TraverseExpression(binaryExpr.Left), TraverseExpression(binaryExpr.Right));
        }
        else if (expr.NodeType == ExpressionType.LessThan)
        {
            var binaryExpr = (BinaryExpression)expr;
            Debug.Log("Binary Expression: " + binaryExpr.NodeType);
            ret = new Less(TraverseExpression(binaryExpr.Left), TraverseExpression(binaryExpr.Right));
        }
        else if (expr.NodeType == ExpressionType.LessThanOrEqual)
        {
            var binaryExpr = (BinaryExpression)expr;
            Debug.Log("Binary Expression: " + binaryExpr.NodeType);
            ret = new LessEqual(TraverseExpression(binaryExpr.Left), TraverseExpression(binaryExpr.Right));
        }
        else if (expr.NodeType == ExpressionType.Equal)
        {
            var binaryExpr = (BinaryExpression)expr;
            Debug.Log("Binary Expression: " + binaryExpr.NodeType);
            ret = new Equal(TraverseExpression(binaryExpr.Left), TraverseExpression(binaryExpr.Right));
        }
        else if (expr.NodeType == ExpressionType.Add)
        {
            var binaryExpr = (BinaryExpression)expr;
            Debug.Log("Binary Expression: " + binaryExpr.NodeType);
            ret = new Add(TraverseExpression(binaryExpr.Left), TraverseExpression(binaryExpr.Right));
        }
        else if (expr.NodeType == ExpressionType.Multiply)
        {
            var binaryExpr = (BinaryExpression)expr;
            Debug.Log("Binary Expression: " + binaryExpr.NodeType);
            ret = new Mul(TraverseExpression(binaryExpr.Left), TraverseExpression(binaryExpr.Right));
        }
        else if (expr.NodeType == ExpressionType.Subtract)
        {
            var binaryExpr = (BinaryExpression)expr;
            Debug.Log("Binary Expression: " + binaryExpr.NodeType);
            ret = new Subtract(TraverseExpression(binaryExpr.Left), TraverseExpression(binaryExpr.Right));
        }
        else if (expr.NodeType == ExpressionType.Divide)
        {
            var binaryExpr = (BinaryExpression)expr;
            Debug.Log("Binary Expression: " + binaryExpr.NodeType);
            ret = new Div(TraverseExpression(binaryExpr.Left), TraverseExpression(binaryExpr.Right));
        }
        else if (expr.NodeType == ExpressionType.Parameter)
        {
            var paramExpr = (ParameterExpression)expr;
            Debug.Log("Parameter: " + paramExpr.Name + " Type: " + paramExpr.Type);
        }
        else if (expr.NodeType == ExpressionType.Constant)
        {
            var constExpr = (ConstantExpression)expr;
            Debug.Log("Constant: " + constExpr.Value + " Type: " + constExpr.Type);
            if (constExpr.Value is int)
                ret = new I((int)constExpr.Value);
            else if (constExpr.Value is float)
                ret = new I((float)constExpr.Value);
        }
        else if (expr.NodeType == ExpressionType.MemberAccess)
        {
            var memberExpr = (MemberExpression)expr;
            Debug.Log("Member Access: " + memberExpr.Member.Name);
            TraverseExpression(memberExpr.Expression);
        }
        else if (expr.NodeType == ExpressionType.Call)
        {
            var callExpr = (MethodCallExpression)expr;
            Debug.Log("Method Call: " + callExpr.Method.Name);
            foreach (var arg in callExpr.Arguments)
            {
                TraverseExpression(arg);
            }
            TraverseExpression(callExpr.Object);
        }
        else
        {
            Debug.Log("Unhandled expression type: " + expr.NodeType);
        }
        return ret;
    }
    [Button]
    public void AddResource(string name,ObjEnum objEnum,int num)
    {
        var obj = (BuildingObj)GameArchitect.get.tableAsset.tableSaver.objs.Find((x) => { return x.name == name; });
        if (obj == null)
            Debug.Log("没有");
        else
            obj.resource.Add(objEnum, num);
    }

    /// <summary>
    /// 更新npc的行为
    /// </summary>
    /// <returns></returns>
    public IEnumerator UpdataNPC()
    {
        for (int i = 0; i < GameArchitect.npcs.Count; i++)//遍历每个PersonObj
        {
            var npc = GameArchitect.npcs[i];
            //如果当前有工作，则直接找工作
            if (npc.HasWork())
            {
                var execWork = npc.lifeStyle.code.GetNowWorks();//获取一系列待执行的活动
                foreach (var work in execWork)
                {
                    yield return StartCoroutine(work.activity.GetNPCAct().Run());//等待这个协程执行
                }
            }
            else
            {
                //先不管
            }
        }
        yield break;
    }
    [Button]
    public void TestZScoreCalculation()
    {
        double[] values = { 100, 200, 300, 400, 500 };

        // 使用 MathNet.Numerics 直接计算 Z-Scores
        double[] zScores = ZScores(values);

        // 手动计算的Z-Score预期结果
        double mean = Statistics.Mean(values);
        double stdDev = Statistics.StandardDeviation(values);
        double[] expectedZScores =
        {
            (100 - mean) / stdDev,
            (200 - mean) / stdDev,
            (300 - mean) / stdDev,
            (400 - mean) / stdDev,
            (500 - mean) / stdDev
        };
        // 断言测试
        for (int i = 0; i < zScores.Length; i++)
        {
            Debug.Log(zScores[i]);
        }
    }
    public void NPCUpdateCircle()
    {
        StartCoroutine(UpdataNPC());
    }
    /// <summary>
    /// 初始角色可选对象
    /// </summary>
    /// <param name="beginSelectEvent"></param>
    /// <returns></returns>
    public async void BeginSelectEvent(BeginSelectEvent beginSelectEvent)
    {
        //Debug.Log(789);
        var optionModel = GameArchitect.Interface.GetModel<PersonObjsOptionModel>();
        var modelset = GameArchitect.Interface.GetModel<TableModelSet>();
        //遍历每一个Person的决策
        for (int i=0;i<GameArchitect.PersonObjs.Count;i++)//遍历每个PersonObj
        {
            var PersonObj=GameArchitect.PersonObjs[i];//当前角色
            GameArchitect.nowPersonObj = PersonObj;//当前角色
            if (!PersonObj.hasSelect)//没有任务就初始化可执行的活动
            {
                var result = new Dictionary<Obj, CardInf[]>();
                for (int j = 0; j < PersonObj.belong.objs.Count; j++)
                {
                    List<CardInf> cardInfList = new List<CardInf>();
                    for (int l = 0; l < PersonObj.belong.objs[j].activities.Count; l++)
                    {
                        if (PersonObj.belong.objs[j].activities[l].Condition(PersonObj.belong.objs[j], PersonObj))
                        {
                            var res = PersonObj.belong.objs[j].activities[l].OutputSelect(PersonObj, PersonObj.belong.objs[j]);//一个可选项
                            cardInfList.Add(res);
                        }
                    }
                    result[PersonObj.belong.objs[j]] = cardInfList.ToArray();
                }
                MainDispatch.Instance().Enqueue(
                () =>
                {
                    optionModel.SetPersonObjOption(PersonObj, result);//当前的行为
                });
                await GameArchitect.Interface.GetModel<ThinkModelSet>().thinks[PersonObj].BeginThink(result);//等待思考完成
            }
        }
        //更新角色活动,每个角色都执行一遍
        foreach(var PersonObj in GameArchitect.PersonObjs)
        {
            tcs = new TaskCompletionSource<bool>(false);
            MainDispatch.Instance().Enqueue(
                () =>
                {
                    hasTime = true;
                    this.SendEvent<BeginActEvent>(new BeginActEvent(PersonObj));
                }
            );
            await tcs.Task;
        }
        MainDispatch.Instance().Enqueue(
            () =>
            {
                //更新时间
                this.SendEvent<EnvirUpdateEvent>(new EnvirUpdateEvent());
            });
    }
    [Button]
    public void CreatePersonObj(bool isplayer, string name, bool sex, string SceneName)
    {
        var Sc = ((GameArchitect)GameArchitect.Interface).tableAsset.tableSaver.tables.Find(e => { return e.TableName == SceneName; });
        CreatePersonObj(isplayer, name, sex, Sc);
    }
    public void CreatePersonObj(bool isplayer, string name, bool sex, TableModel Sc)
    {
        var s = Tool.DeepClone<PersonSaver>((PersonSaver)Map.Instance.GetSaver(typeof(PersonObj)));
        s.isPlayer = isplayer;
        s.PersonObjName =name;
        var p = new PersonObj(s);
        p.sex = sex;
        if (Sc == null)
        {
            Debug.Log("无法创建");
            return;
        }
        ((GameArchitect)GameArchitect.Interface).tableAsset.tableSaver.PersonObjList.Add(p);//创建对象
        ((GameArchitect)GameArchitect.Interface).GetModel<PersonObjsOptionModel>().AddPersonObj(p);
        ((GameArchitect)GameArchitect.Interface).GetModel<ThinkModelSet>().AddThinkMode(p);
        Sc.EnterTable(p);
    }
    /// <summary>
    /// 创建一个床
    /// </summary>
    [Button]
    public void CreateBed(string SceneName)
    {
        var s = Tool.DeepClone<BedSaver>((BedSaver)Map.Instance.GetSaver(typeof(BedObj)));
        var p = new BedObj(s);
        var Sc = ((GameArchitect)GameArchitect.Interface).tableAsset.tableSaver.tables.Find(e => { return e.TableName == SceneName; });
        if (Sc == null)
        {
            Debug.Log("无法创建");
            return;
        }
        Sc.AddToTable(p);
    }
    [Button]
    public void CreateDesk(string SceneName)
    {
        var s = Tool.DeepClone<DeskSaver>((DeskSaver)Map.Instance.GetSaver(typeof(DeskObj)));
        var p = new DeskObj(s);
        var Sc = ((GameArchitect)GameArchitect.Interface).tableAsset.tableSaver.tables.Find(e => { return e.TableName == SceneName; });
        if (Sc == null)
        {
            Debug.Log("无法创建");
            return;
        }
        Sc.AddToTable(p);
    }
    [Button]
    public void CreateRestr(string SceneName)
    {
        var s = Tool.DeepClone<RestaurantSaver>((RestaurantSaver)Map.Instance.GetSaver(typeof(RestaurantObj)));
        var p = new RestaurantObj(s);
        var Sc = ((GameArchitect)GameArchitect.Interface).tableAsset.tableSaver.tables.Find(e => { return e.TableName == SceneName; });
        if (Sc == null)
        {
            Debug.Log("无法创建");
            return;
        }
        Sc.AddToTable(p);
    }
    //[Button]
    //public void CreatePath(PathObj pathObj,string SceneName)
    //{
    //    var pathIbj = (PathObj)Activator.CreateInstance(pathObj.GetType(), new object[] { new PathSaver() });
    //    var Sc = ((GameArchitect)GameArchitect.Interface).tableAsset.tableSaver.tables.Find(e => { return e.TableName == SceneName; });
    //    if (Sc == null)
    //    {
    //        Debug.Log("无法创建");
    //        return;
    //    }
    //    Sc.AddToTable(pathIbj);
    //}
    [Button]
    public void DestoryObj(string objName,string tableName)
    {
        var Sc = ((GameArchitect)GameArchitect.Interface).tableAsset.tableSaver.tables.Find(e => { return e.TableName == tableName; });
        if (Sc == null)
        {
            Debug.Log("无法创建");
            return;
        }
        var x=Sc.objs.Find(e => { return objName == e.name; });
        Sc.RemoveToTable(x);
    }
    public struct CreatePathObj
    {
        public string y;
        public int time;
    }
    [Button]
    public void CreatePath(string objName, string SceneName)
    {
        var s = Tool.DeepClone<PathSaver>((PathSaver)Map.Instance.GetSaver(ObjEnum.PathObjE));
        var p = new PathObj(s);
        p.cardInf.title = objName;
        p.cardInf.description = "PathToOtherScene";
        p.activities = new List<Activity>();
        var Sc = ((GameArchitect)GameArchitect.Interface).tableAsset.tableSaver.tables.Find(e => { return e.TableName == SceneName; });
        if (Sc == null)
        {
            Debug.Log("无法创建");
            return;
        }
        Sc.AddToTable(p);
    }
    [Button]
    public void DestoryPath(string objName, string tableName)
    {
        var Sc = ((GameArchitect)GameArchitect.Interface).tableAsset.tableSaver.tables.Find(e => { return e.TableName == tableName; });
        if (Sc == null)
        {
            Debug.Log("无法创建");
            return;
        }
        var x = (PathObj)Sc.objs.Find(e => { return objName == e.name; });
        x.activities.RemoveAll(e => { return ((Go)e).xname == Sc.TableName; });//删除所有目的地节点
        Sc.RemoveToTable(x);
    }
    /// <summary>
    /// 创建农场
    /// </summary>
    /// <param name="size"></param>
    /// <param name="tableName"></param>
    [Button]
    public void CreateFarm(FarmSaver farmSaver,string tableName)
    {
        var p = new FarmObj(farmSaver);
        var Sc = ((GameArchitect)GameArchitect.Interface).tableAsset.tableSaver.tables.Find(e => { return e.TableName == tableName; });
        if (Sc == null)
        {
            Debug.Log("无法创建");
            return;
        }
        Sc.AddToTable(p);
    }
}
