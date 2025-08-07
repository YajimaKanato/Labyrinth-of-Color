using UnityEngine;

public class CreateLabyrinth : MonoBehaviour
{
    [Header("LabyrinthSize")]
    [SerializeField, Tooltip("�������̃T�C�Y")] int _labyrinthSizeX = 5;
    [SerializeField, Tooltip("������̃T�C�Y")] int _labyrinthSizeY = 5;
    [SerializeField, Tooltip("�K�w��")] int _labyrinthSizeZ = 5;

    int[,,] _roomID;
    /// <summary>
    /// 0����
    /// </summary>
    public int[,,] RoomID { get { return _roomID; } }

    //�v���C���[���A�N�V�������s���Ƃ�����u�����v�Ƃ���

    private void Awake()
    {
        //�������ŏ��ɖ��{����
        LabyrinthCreate();
        for (int i = 0; i < _roomID.GetLength(0); i++)
        {
            for (int j = 0; j < _roomID.GetLength(1); j++)
            {
                for (int k = 0; k < _roomID.GetLength(2); k++)
                {
                    Debug.Log(_roomID[i, j, k]);
                }
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void LabyrinthCreate()
    {
        //�A���S���Y���𑖂点��Ƃ��̃T�C�Y�ɒ���
        int xlen = _labyrinthSizeX * 2 + 1;
        int ylen = _labyrinthSizeY * 2 + 1;
        int zlen = _labyrinthSizeZ * 2 + 1;

        //���⒌�ɓ����镔�����܂߂��T�C�Y�ɒ���
        _roomID = new int[xlen, ylen, zlen];

        //�����ɔԍ��𓖂ĂĂ���
        for (int n = 0; n < zlen; n++)
        {
            for (int m = 0; m < ylen; m++)
            {
                for (int l = 0; l < xlen; l++)
                {
                    if (l * m * n % 2 == 1)
                    {
                        _roomID[l, m, n] = n * ylen * xlen + m * xlen + l;
                    }
                }
            }
        }

        //�����_���ŕǂ��Ƃ�
        for (int i = 0; i < (xlen / 2) * (ylen / 2) * (zlen / 2) - 1; i++)
        {
            int randx, randy, randz;
            int change;
            int id1, id2;
            int randxx = 0, randyy = 0, randzz = 0, randxxx = 0, randyyy = 0, randzzz = 0;
            bool tf;
            do
            {
                tf = false;
                change = 0;

                //�����_���ɑI�񂾏ꏊ�������ɂȂ�܂ŌJ��Ԃ�
                do
                {
                    randx = Random.Range(1, xlen - 1);
                    randy = Random.Range(1, ylen - 1);
                    randz = Random.Range(1, zlen - 1);
                } while (randx * randy * randz % 2 != 1);

                //6�����ɑ΂��ăA�v���[�`
                int[] face = { 1, 2, 3, 4, 5, 6 };
                for (int j = 0; j < 6; j++)
                {
                    int bre = 0;
                    int rand2;

                    //�U�����̂ǂ�����I�Ԃ��A���łɑI�񂾂Ƃ���͑I�΂Ȃ�
                    do
                    {
                        rand2 = Random.Range(0, face.Length);
                    } while (face[rand2] == 0);

                    switch (face[rand2] - 1)
                    {
                        case 0:
                            if (randx != xlen - 2)
                            {
                                if (_roomID[randx + 2, randy, randz] != _roomID[randx, randy, randz])
                                {
                                    randxx = randx + 2;
                                    randyy = randy;
                                    randzz = randz;
                                    randxxx = randx + 1;
                                    randyyy = randy;
                                    randzzz = randz;
                                    tf = true;
                                }
                                else
                                {
                                    change++;
                                }
                            }
                            else
                            {
                                change++;
                            }
                            break;
                        case 1:
                            if (randx != 1)
                            {
                                if (_roomID[randx - 2, randy, randz] != _roomID[randx, randy, randz])
                                {
                                    randxx = randx - 2;
                                    randyy = randy;
                                    randzz = randz;
                                    randxxx = randx - 1;
                                    randyyy = randy;
                                    randzzz = randz;
                                    tf = true;
                                }
                                else
                                {
                                    change++;
                                }
                            }
                            else
                            {
                                change++;
                            }
                            break;
                        case 2:
                            if (randy != ylen - 2)
                            {
                                if (_roomID[randx, randy + 2, randz] != _roomID[randx, randy, randz])
                                {
                                    randxx = randx;
                                    randyy = randy + 2;
                                    randzz = randz;
                                    randxxx = randx;
                                    randyyy = randy + 1;
                                    randzzz = randz;
                                    tf = true;
                                }
                                else
                                {
                                    change++;
                                }
                            }
                            else
                            {
                                change++;
                            }
                            break;
                        case 3:
                            if (randy != 1)
                            {
                                if (_roomID[randx, randy - 2, randz] != _roomID[randx, randy, randz])
                                {
                                    randxx = randx;
                                    randyy = randy - 2;
                                    randzz = randz;
                                    randxxx = randx;
                                    randyyy = randy - 1;
                                    randzzz = randz;
                                    tf = true;
                                }
                                else
                                {
                                    change++;
                                }
                            }
                            else
                            {
                                change++;
                            }
                            break;
                        case 4:
                            if (randz != zlen - 2)
                            {
                                if (_roomID[randx, randy, randz + 2] != _roomID[randx, randy, randz])
                                {
                                    randxx = randx;
                                    randyy = randy;
                                    randzz = randz + 2;
                                    randxxx = randx;
                                    randyyy = randy;
                                    randzzz = randz + 1;
                                    tf = true;
                                }
                                else
                                {
                                    change++;
                                }
                            }
                            else
                            {
                                change++;
                            }
                            break;
                        case 5:
                            if (randz != 1)
                            {
                                if (_roomID[randx, randy, randz - 2] != _roomID[randx, randy, randz])
                                {
                                    randxx = randx;
                                    randyy = randy;
                                    randzz = randz - 2;
                                    randxxx = randx;
                                    randyyy = randy;
                                    randzzz = randz - 1;
                                    tf = true;
                                }
                                else
                                {
                                    change++;
                                }
                            }
                            else
                            {
                                change++;
                            }
                            break;
                    }

                    if (tf == true)
                    {
                        id1 = _roomID[randx, randy, randz];
                        id2 = _roomID[randxx, randyy, randzz];
                        for (int n = 0; n < zlen; n++)
                        {//�L���[�u�ɔԍ��𓖂ĂĂ���
                            for (int m = 0; m < ylen; m++)
                            {
                                for (int l = 0; l < xlen; l++)
                                {
                                    if (l % 2 == 1 && m % 2 == 1 && n % 2 == 1)
                                    {
                                        if (_roomID[l, m, n] == id1 || _roomID[l, m, n] == id2)
                                        {
                                            _roomID[l, m, n] = _roomID[randx, randy, randz];
                                        }
                                    }
                                }
                            }
                        }
                        _roomID[randxxx, randyyy, randzzz] = 1;
                        bre = 1;
                    }
                    if (bre == 1) break;

                    face[rand2] = 0;
                }
            } while (change == 6);
        }
    }
}