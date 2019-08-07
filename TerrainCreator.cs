using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(MeshFilter))]
public class TerrainCreator : MonoBehaviour
{
    Mesh mesh;
   private Vector3[] vertices;
    private int [] triangulos;
    public int Sizex = 20;
    public int Sizez = 20;
    public  int x=0;
  public int z=0;
  public int i = 0;
  public int j = 0;
    public double n = 0;
    public float y = 0;
    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh; // lo añades pero autogenera la "figura".
        vertices = mesh.vertices;
        mesh.triangles = triangulos;
        mesh.RecalculateBounds();//calcular el volumen delimitador de la malla a partir de los vértices.
    }

    private void Start()
    {
      
       StartCoroutine(CalulateShape());
       
    }
    private void Update()
    {
       
        UpdateDraw();

    }
    private void  UpdateDraw()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangulos;
        mesh.RecalculateBounds();//calcular el volumen delimitador de la malla a partir de los vértices.
    }
    IEnumerator CalulateShape()
    {
        vertices = new Vector3[(x + 2) * (z + 2)]; // el +2 es porque la clase vector3 pcuneta como 3 posiciones en el array [1]={0,1,2},{3,4,5};
        for (x = 0; x < Sizex; x++)
        {
            for (z = 0; z < Sizez; z++)
            {
                n = Math.Sin(x)*Math.Cos(z-1);
               y= Convert.ToSingle(n);
                vertices[i++] = new Vector3(x, y, z);

            }
        }

        triangulos = new int[Sizex * Sizez * 9];
        for (int lado = 0, vertice = 0, y = 0; y < Sizez+1; y++, vertice++)
        {
            for (int x = 0; x < Sizex+1; x++, lado += 6, vertice++)
            {
                triangulos[lado] = vertice;
                triangulos[lado + 3] = triangulos[lado + 2] = vertice + 1;
                triangulos[lado + 4] = triangulos[lado + 1] = vertice + Sizex + 1;
                triangulos[lado + 5] = vertice + Sizex + 2;
                
            }
        }
    
            yield return new WaitForSeconds(.1f);

        
    }
        
    
  private void OnDrawGizmos()
    {
        if (vertices == null)
        {

            Debug.Log("No Data");
        }
        else
        {
            for (i = 0; i < vertices.Length; i++)
            {
                Gizmos.DrawSphere(vertices[i], .1f);
            }
        }
    }

    }