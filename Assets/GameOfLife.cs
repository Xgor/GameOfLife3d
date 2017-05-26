using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameOfLife : MonoBehaviour {

	public GameObject gridObj;
	public int width,height,depth;
	List<GameObject> grid;

	public float updateTime;
	float timer;

	public int MinNeighbourSpawn,MaxNeighbourSpawn,
	MinNeighbourDeath, MaxNeighbourDeath; 
	// Use this for initialization
	void Start () {

		grid = new List<GameObject>();
		Vector3 objPos = Vector3.zero;
		for(int x = 0; x < width;x++)
		{
			objPos.x = x-width/2.0f;
			for(int y = 0; y < height;y++)
			{
				objPos.y = y-height/2.0f;
				for(int z = 0; z < depth;z++)
				{
					objPos.z = z-depth/2.0f;
					GameObject o = (GameObject)Instantiate(gridObj,objPos,Quaternion.identity);


					grid.Add(o);

				}
			}
		}
		CreateNew();
	}

	void CreateNew()
	{
		foreach(GameObject obj in grid)
		{
			if( Random.value > 0.5f)
				ActivateCell(obj);
			else
				obj.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			CreateNew();
		}

	//	grid[0].SetActive(!grid[0].activeSelf);
		timer += Time.deltaTime;
		if(updateTime <timer)
		{
			timer-= updateTime;
			Iteration();
		}
	}

	void Iteration()
	{
		for(int x = 0; x < width;x++)
		{
			for(int y = 0; y < height;y++)
			{
				for(int z = 0; z < depth;z++)
				{
					int neighbours =GetNeighbours(x,y,z);

					GameObject currObj = GetObject(x,y,z);


					if(currObj.activeSelf)
					{
						if(MinNeighbourDeath >= neighbours || MaxNeighbourDeath <= neighbours)
						{
							currObj.SetActive(false);
						}
						else
						{
							Material mat = GetMaterial(currObj);
							Color c = mat.color;
							c.r += 0.2f;
							c.g -= 0.2f;
							mat.color = c;
						}
					}
					else
					{
						if(MinNeighbourSpawn <= neighbours && MaxNeighbourSpawn >= neighbours)
						{
							ActivateCell(currObj);
						}
					}


				}
			}
		}
	}
	Material GetMaterial(GameObject obj)
	{
		return obj.GetComponent<MeshRenderer>().material;
	}

	void ActivateCell(GameObject obj)
	{
		obj.SetActive(true);
		Material mat = GetMaterial(obj);
		mat.color = Color.green;
	}

	GameObject GetObject(int x,int y,int z)
	{
		return grid[x+
			width*y+
			height*width*z];
	}

	int GetNeighbours(int x,int y,int z)
	{
		int neighbours = 0;
		for(int xx = x-1; xx < x+2;xx++)
		{
			for(int yy = y-1; yy < y+2;yy++)
			{
				for(int zz = z-1; zz < z+2;zz++)
				{
					if(xx>=0&&yy>=0&&zz>=0&&xx<width&&yy<height&&zz<depth)
					{
						if(GetObject(xx,yy,zz).activeSelf)
							neighbours++;
					}
				}

			} 

		}
		return neighbours;
	}
}
