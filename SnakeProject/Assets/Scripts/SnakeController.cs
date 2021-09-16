using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeController : MonoBehaviour {
    public static float _MoveSpeed;
    public float _SteerSpeed = 180;
    public static int _Gap = 10;
	public static int Difficulty;
	int foodCount = 0;
	Vector3 _BodyPosition;
	[SerializeField]
    private GameObject BodyPrefab;	
	[SerializeField]
    private GameObject BodyParent;
	[SerializeField]
	private GameObject _Collectibles;	
	[SerializeField]
	private GameObject _Bonus;	
	[SerializeField]
	private GameObject _Minus;
	GameObject _Tail;
	[SerializeField] float minX, maxX, minZ, maxZ;
	[SerializeField]
	ParticleSystem ParticleSystem;

	private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();
	// Start is called before the first frame update
	private void Start()
	{
		Spawner();
	}

	// Update is called once per frame
	void Update() {
		SnakeMove(_MoveSpeed);
		SnakeBodyMove();
	}
	#region Yılanın Kontrolü ve Beden Takibi
	private void SnakeMove(float _Speed) // Hareket kontrol sistemi
	{
		transform.position += transform.forward * _Speed * Time.deltaTime;
		float steerDirection = Input.GetAxis("Horizontal");
		transform.Rotate(Vector3.up* steerDirection * _SteerSpeed * Time.deltaTime);
	}
	private void SnakeBodyMove() // Yılanın vücudunun takip sistemi
	{
		AddListObject();
		PositionsHistory.Insert(0, transform.position);
		int index = 0;
		foreach (var body in BodyParts)
		{
			Vector3 point = PositionsHistory[Mathf.Clamp(index * _Gap, 0, PositionsHistory.Count - 1)];
			Vector3 moveDirection = point - body.transform.position;
			body.transform.position += moveDirection * (_MoveSpeed+2) * Time.deltaTime;
			body.transform.LookAt(point);
			_BodyPosition = point;
			index++;
		}
	}
	#endregion
	public void GrowSnake() // Yılanın vücut parçalarının oluşumu
	{
		var body = Instantiate(BodyPrefab, _BodyPosition, Quaternion.identity);
		body.transform.parent = BodyParent.transform;
		BodyParts.Add(body);
		_Tail = body;
		StartCoroutine(DelayBody());
	}
	public void Spawner() // Bu fonksiyonu Collectiblesdan çekerek yeni nesne üretiyorum
	{
		if (foodCount==5)
		{
			ParticleSystem.Play();
			var spawn = Instantiate(_Bonus, new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ)), Quaternion.identity);
			spawn.transform.parent = transform.parent;
			foodCount = 0;
		}
		else
		{
			ParticleSystem.Play();
			var spawn = Instantiate(_Collectibles, new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ)), Quaternion.identity);
			spawn.transform.parent = transform.parent;
			foodCount++;
		}
		if (foodCount==3)
		{
			ParticleSystem.Play();
			var spawn = Instantiate(_Minus, new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ)), Quaternion.identity);
			spawn.transform.parent = transform.parent;
			foodCount++;
		}

	}
	#region Parça eksiltme sistemi, Listeyi sürekli yenileyerek takip eden parçaların nullreference vermesini önlüyoruz.
	private void AddListObject()
	{
		BodyParts.Clear();
		foreach (Transform child in BodyParent.transform)
		{
			BodyParts.Add(child.gameObject);
		}
	}

	public void RemoveObject()
	{
		var BodyPartSnake = BodyParts[BodyParts.Count-1];
		Destroy(BodyPartSnake);
	}

	#endregion
	IEnumerator DelayBody()
	{
		yield return new WaitForSeconds(1f);
		_Tail.AddComponent<Obstacles>();
	}
}