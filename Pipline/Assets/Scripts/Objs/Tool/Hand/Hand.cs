using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 保存这类商品的具体信息
/// </summary>
public class HandInf : ToolInf
{
}

public class HandObj : ToolObj<HandInf>
{
	public override Dictionary<ProductivityEnum, int> GetProducs()
	{
		return get().dics;
	}
}
