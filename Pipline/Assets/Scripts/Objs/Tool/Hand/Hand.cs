using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������Ʒ�ľ�����Ϣ
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
