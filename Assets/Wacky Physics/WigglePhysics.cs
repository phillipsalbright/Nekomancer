using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WigglePhysics : MonoBehaviour {

	public float springForce = 20f;
	public float damping = 5f;
	public float force = 10f;
	public float forceOffset = 0.1f;
	public int stepSkip = 10;

	Mesh deformingMesh;
	Vector3[] originalVertices, displacedVertices;
	Vector3[] vertexVelocities;
	Vector3[] normals;

	float uniformScale = 1f;

	void Start () {
		deformingMesh = GetComponent<MeshFilter>().mesh;
		originalVertices = deformingMesh.vertices;
		normals = deformingMesh.normals;
		displacedVertices = new Vector3[originalVertices.Length];
		for (int i = 0; i < originalVertices.Length; i++) {
			displacedVertices[i] = originalVertices[i];
		}
		vertexVelocities = new Vector3[originalVertices.Length];
	}

	void Update () {
		RaycastHit hit;
		for (int i = 0; i < originalVertices.Length; i++)
        {
			int wiggle = Random.Range(0, stepSkip);
			if (wiggle == 1)
            {
				Vector3 point = originalVertices[i];
				point += transform.position - transform.TransformPoint(originalVertices[i]) * forceOffset;
				AddDeformingForce(point, force * Random.Range(0f, 1f));
			}
//			if (Physics.Raycast(transform.TransformPoint(originalVertices[i]), transform.TransformPoint(originalVertices[i]) - transform.position, out hit))
//			{
//				if (hit.collider.gameObject != this.gameObject)
//				{
//					float offsetDistance = hit.distance;
//					if (offsetDistance < 0.5)
//					{
//						Debug.DrawLine(transform.position, hit.point, Color.cyan);
//						Vector3 point = hit.point;
//						point += hit.normal * forceOffset;
//						AddDeformingForce(point, force);
//					}
//				}
//			}
		}
		uniformScale = transform.localScale.x;
		for (int i = 0; i < displacedVertices.Length; i++) {
			UpdateVertex(i);
		}
		deformingMesh.vertices = displacedVertices;
		deformingMesh.RecalculateNormals();
	}

	void UpdateVertex (int i) {
		Vector3 velocity = vertexVelocities[i];
		Vector3 displacement = displacedVertices[i] - originalVertices[i];
		displacement *= uniformScale;
		velocity -= displacement * (springForce * Random.Range(1, 10)) * Time.deltaTime;
		velocity *= 1f - damping * Time.deltaTime;
		vertexVelocities[i] = velocity;
		displacedVertices[i] += velocity * (Time.deltaTime / uniformScale);
	}

	public void AddDeformingForce (Vector3 point, float force) {
		point = transform.InverseTransformPoint(point);
		for (int i = 0; i < displacedVertices.Length; i++) {
			AddForceToVertex(i, point, force);
		}
	}

	void AddForceToVertex (int i, Vector3 point, float force) {
		Vector3 pointToVertex = displacedVertices[i] - point;
		pointToVertex *= uniformScale;
		float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
		float velocity = attenuatedForce * Time.deltaTime;
		vertexVelocities[i] += pointToVertex.normalized * velocity;
	}
}