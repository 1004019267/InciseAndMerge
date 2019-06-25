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

public class Example : MonoBehaviour
{
    void Start()
    {

        //合并网格
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            //矩阵自身坐标的点转化为世界左边的点 并且要保持相对位置不变，要转换为父节点的本地坐标(防止合并后位置偏移)
            combine[i].transform = transform.worldToLocalMatrix * meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }

        //false表示不是合并为一个网格而是一个网格列表
        gameObject.AddComponent<MeshFilter>().mesh.CombineMeshes(combine,false);

        var ver3=  gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;
        Vector2[] ver2 = new Vector2[ver3.Length];
        for (int i = 0; i < ver3.Length; i++)
        {
            ver2[i] = ver3[i];
        }
        gameObject.AddComponent<PolygonCollider2D>().points = ver2;
        //合并材质
        Material[] mats = new Material[meshRenderers.Length];
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i] = meshRenderers[i].sharedMaterial;
        }

        gameObject.AddComponent<MeshRenderer>().sharedMaterials = mats;

        transform.gameObject.SetActive(true);


    }
}
