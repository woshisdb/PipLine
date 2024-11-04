using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IncomeType
{
    FixedSalary,       // 固定工资
    SelfEmployment,    // 自营业收入,自己干
    SelfAndOtherEmployment,//自己和其他人同时工作
    CapitalGains,      // 资本收益,会通过进口出口,雇佣劳动的方式收入
}