using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Taper : MonoBehaviour
{
	public float m_RadiusStart = 2f;
	public float m_RadiusEnd = 0f;
	public float m_Height = 4f;
	public int m_RadialSegmentCount = 64;
	public int m_HeightSegmentCount = 16;
	public bool m_DynamicGenerate = true;
	
	private List<Vector3> m_Vertices = new List<Vector3>();
	private List<Vector3> m_Normals = new List<Vector3>();
	private List<Vector2> m_Texcoords = new List<Vector2>();
	private List<int> m_Triangles = new List<int>();
	private Mesh m_Mesh = null;

	private void AddTriangle(int ind0, int ind1, int ind2)
	{
		m_Triangles.Add(ind0);
		m_Triangles.Add(ind1);
		m_Triangles.Add(ind2);
	}
	private void BuildRing(int segmentCount, Vector3 centre, float radius, bool buildTriangles, Vector2 slope, float vCoord)
	{
		float angleInc = (Mathf.PI * 2.0f) / segmentCount;

		for (int i = 0; i <= segmentCount; i++)
		{
			float angle = angleInc * i;

			Vector3 unitPosition = Vector3.zero;
			unitPosition.x = Mathf.Cos(angle);
			unitPosition.z = Mathf.Sin(angle);

			float normalVertical = -slope.x;
			float normalHorizontal = slope.y;

			Vector3 normal = unitPosition * normalHorizontal;
			normal.y = normalVertical;

			m_Vertices.Add(centre + unitPosition * radius);
			m_Normals.Add(normal);
			m_Texcoords.Add(new Vector2((float)i / segmentCount, vCoord));

			if (i > 0 && buildTriangles)
			{
				int baseIndex = m_Vertices.Count - 1;

				int vertsPerRow = segmentCount + 1;

				int index0 = baseIndex;
				int index1 = baseIndex - 1;
				int index2 = baseIndex - vertsPerRow;
				int index3 = baseIndex - vertsPerRow - 1;

				AddTriangle(index0, index2, index1);
				AddTriangle(index2, index3, index1);
			}
		}
	}
	public void Build(float radiusStart, float radiusEnd, float height, int radialSegments, int heightSegments)
	{
		m_Vertices.Clear();
		m_Normals.Clear();
		m_Texcoords.Clear();
		m_Triangles.Clear();
		
		float heightInc = height / heightSegments;

		Vector2 slope = new Vector2(radiusEnd - radiusStart, height);
		slope.Normalize();

		for (int i = 0; i <= heightSegments; i++)
		{
			Vector3 centrePos = Vector3.up * heightInc * i;
			float radius = Mathf.Lerp(radiusStart, radiusEnd, (float)i / heightSegments);
			float v = (float)i / heightSegments;
			BuildRing(radialSegments, centrePos, radius, i > 0, slope, v);
		}

		if (m_Mesh == null)
		{
			m_Mesh = new Mesh();
			m_Mesh.name = "Taper";
		}
		else
		{
			m_Mesh.Clear();
		}
		
		m_Mesh.vertices = m_Vertices.ToArray();
		m_Mesh.normals = m_Normals.ToArray();
		m_Mesh.uv = m_Texcoords.ToArray();
		m_Mesh.triangles = m_Triangles.ToArray();
		m_Mesh.RecalculateBounds();
	}
	private void Start ()
	{
		if (!m_DynamicGenerate)
		{
			Build(m_RadiusStart, m_RadiusEnd, m_Height, m_RadialSegmentCount, m_HeightSegmentCount);
		
			MeshFilter filter = GetComponent<MeshFilter>();
			if (filter != null)
				filter.mesh = m_Mesh;
		}
	}
	private void Update ()
	{
		if (m_DynamicGenerate)
		{
			Build(m_RadiusStart, m_RadiusEnd, m_Height, m_RadialSegmentCount, m_HeightSegmentCount);
		
			MeshFilter filter = GetComponent<MeshFilter>();
			if (filter != null)
				filter.mesh = m_Mesh;
		}
	}
}
