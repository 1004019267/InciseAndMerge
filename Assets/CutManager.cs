/**
 *Copyright(C) 2019 by #COMPANY#
 *All rights reserved.
 *FileName:     #SCRIPTFULLNAME#
 *Author:       #AUTHOR#
 *Version:      #VERSION#
 *UnityVersion：#UNITYVERSION#
 *Date:         #DATE#
 *Description:   
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutManager
{
    //相机必须为正交不然没有用
    /// <summary>
    /// 添加切割线
    /// </summary>
    /// <param name="go"></param>
    public void AddCutControl(GameObject go)
    {
        go.AddComponent<Slicer2DController>().addForce = false;
        SetSlicerType(Slicer2DController.SliceType.Linear);
    }

    /// <summary>
    /// 设置切割线类型
    /// </summary>
    /// <param name="type"></param>
    public void SetSlicerType(Slicer2DController.SliceType type)
    {
        Slicer2DController.instance.sliceType = type;
    }
    /// <summary>
    /// 设置切割线颜色
    /// </summary>
    /// <param name="col"></param>
    public void SetControlLine(Color col)
    {
        Slicer2DController.instance.slicerColor = col;
    }
    /// <summary>
    /// 设置切割线宽度
    /// </summary>
    /// <param name="val"></param>
    public void SetControlLineWidth(float val)
    {
        Slicer2DController.instance.lineWidth = val;
    }

    /// <summary>
    /// 添加可切割物体
    /// </summary>
    /// <param name="go"></param>
    public void AddCutObj(GameObject go)
    {
        go.AddComponent<Slicer2D>();
        var goLine = go.AddComponent<ColliderLineRenderer2D>();
        goLine.lineWidth = 0;
      
    }

}
