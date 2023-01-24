using UnityEngine;
using System.Collections;

public class GenerateRoom : MonoBehaviour
{
    public struct room
    {
        public int roomID;
        public float roomWeight;
    };

    public static int RoomTypeNum = 3;
    public room[] Rooms = new room[RoomTypeNum];

    public static int ArraySize = 100;

    [Space(10)]
    public int BossRoomDis = 5;
    public int NormalRoomDis = 5;
    [HideInInspector]
    public Vector2Int InitRoom = new Vector2Int(50, 50);
    [Space(15)]
    public GameObject Room;
    [Space(15)]
    public bool IsAnimating = true;

    int[,] map = new int[ArraySize, ArraySize];

    float[] dir_weight = new float[4];

    void Start()
    {
        beginGen();
    }

    void beginGen()
    {
        #region 数组初始化
        for (int i = 0; i < ArraySize; i++)
            for (int j = 0; j < ArraySize; j++)
                map[i, j] = 0;
        map[InitRoom.x, InitRoom.y] = -1; //初始位置
        #endregion

        #region 不同房间的权重分配
        Rooms[0].roomID = 1;
        Rooms[0].roomWeight = 0.1f;
        Rooms[1].roomID = 2;
        Rooms[1].roomWeight = 0.8f;
        Rooms[2].roomID = 3;
        Rooms[2].roomWeight = 0.1f;
        #endregion

        #region 四个方向的权重分配（房间延伸的方向）
        for (int i = 0; i < 4; i++) dir_weight[i] = 0;

        int s = (int)Random.Range(1, 100) % 4;
        dir_weight[s] = Random.Range(0.29f, 0.35f);
        float res = 1f - dir_weight[s];
        for (int i = 0; i < 4; i++)
        {
            if (dir_weight[i] == 0)
            {
                dir_weight[i] = Random.Range(0.26f, 0.3f);
                if (dir_weight[i] > res) dir_weight[i] = res;
                res -= dir_weight[i];
            }
        }
        #endregion

        StartCoroutine(genRoom(InitRoom.x, InitRoom.y, 1, true, IsAnimating));
        StartCoroutine(genRoom(InitRoom.x, InitRoom.y, 1, false, IsAnimating));
        StartCoroutine(genRoom(InitRoom.x, InitRoom.y, 1, false, IsAnimating));

    }

    #region 一些函数
    int up = 0, down = 0, right = 0, left = 0;
    Vector2Int dir(int x, int y, int d)
    {
        int p = x, q = y;
        bool flag = false;
        int t = (int)((float)BossRoomDis / 3f + 0.5f);

        if ((right > t) && (left > t) && (up > t) && (down > t)) flag = true;

        if (d == 1 && ((right <= t) || flag)) { p++; right++; }
        if (d == 2 && ((left <= t) || flag)) { p--; left++; }
        if (d == 3 && ((up <= t) || flag)) { q++; up++; }
        if (d == 4 && ((down <= t) || flag)) { q--; down++; }
        return new Vector2Int(p, q);
    }
    int randomRoom()
    {
        int type = 0;
        int seed = ((int)Random.Range(1000, 2000) % 100) + 1; // 1-100
        int index = 0;
        for (int i = 0; i < RoomTypeNum; i++)
        {
            int l = (int)(Rooms[i].roomWeight * 100f);
            if ((seed > index) && (seed < (index + l)))
            {
                type = Rooms[i].roomID;
            }
            index += l;
        }
        return type;
    }
    int randomDir()
    {
        int dir = 0;
        int seed = ((int)Random.Range(1000, 2000) % 100) + 1; // 1-100
        int index = 0;
        for (int i = 0; i < 4; i++)
        {
            int l = (int)(dir_weight[i] * 100f);
            if ((seed > index) && (seed < (index + l)))
            {
                dir = i + 1;
            }
            index += l;
        }
        return dir;
    }

    #endregion

    IEnumerator genRoom(int x, int y, int n, bool boss, bool IsAnim)
    {
        #region 生成房间 以及递归退出

        if (boss)
            if (n > BossRoomDis) //达到Boss房的长度后退出
            {
                if (IsAnim)
                    yield return new WaitForSecondsRealtime(0.25f);
                map[x, y] = -2; //Boss
                GameObject _r = Instantiate(Room, new Vector3((float)(x - InitRoom.x) * 1f, (float)(y - InitRoom.x) * 1f, 0), new Quaternion(), Room.transform.parent);
                if (map[x, y] == -2)
                {
                    _r.gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(186f / 255f, 59f / 255f, 59f / 255f);
                    _r.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(186f / 255f, 59f / 255f, 59f / 255f, 150f / 255f);
                }
                yield break;
            }

        if (!boss)
            if (n > NormalRoomDis) //达到普通房的长度后退出
            {
                yield break;
            }

        if (IsAnim)
            yield return new WaitForSecondsRealtime(0.25f);

        GameObject r = Instantiate(Room, new Vector3((float)(x - InitRoom.x) * 1f, (float)(y - InitRoom.x) * 1f, 0), new Quaternion(), Room.transform.parent);
        if (map[x, y] == -1)
        {
            r.gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(80f / 255f, 162f / 255f, 28f / 255f);
            r.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(80f / 255f, 162f / 255f, 28f / 255f, 70f / 255f);
        }
        if (map[x, y] == 1)
            r.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(90f / 255f, 90f / 255f, 190f / 255f);
        if (map[x, y] == 2)
            r.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(170f / 255f, 120f / 255f, 110f / 255f);
        if (map[x, y] == 3)
            r.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(124f / 255f, 56f / 255f, 65f / 255f);
        
        #endregion

        Vector2Int pos = dir(x, y, randomDir());
        int c = 0;
        while ((map[pos.x, pos.y] != 0) || (map[pos.x, pos.y] == -1)) //不允许与其他重合
        {
            pos = dir(x, y, randomDir());
            c++;
            if (c >= 20) { pos.x = x; pos.y = y; break; }
        }

        map[pos.x, pos.y] = randomRoom();

        StartCoroutine(genRoom(pos.x, pos.y, n + 1, boss, IsAnim));
    }



}
