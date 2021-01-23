<!--
 * @Author: your name
 * @Date: 2021-01-13 20:52:00
 * @LastEditTime: 2021-01-20 18:03:46
 * @LastEditors: Please set LastEditors
 * @Description: In User Settings Edit
 * @FilePath: \BombBoy\Assets\Note\note1.md
-->
## 笔记z
1. tileMap
2. Rule Tile 规则瓦片 Rule Tile 规则瓦片
3. 图层渲染顺序 sortinglayer 不是 layer
4. 对于2d图层的添加碰撞体，可以使用TileMap Collider2D，同时搭配Composite Collider2D,Rigidbody2d改成staic type保证不会和其它物体碰撞。
5. 关于小人等物体。Rigidbody2d+Collider即可，多边形Collider可选PolyGonCollider。对于2d物体需要冻结z轴。
6. Layer的设置
7. prefab
8. 刚体运动
   ```C#
    float horizontalInput = Input.GetAxis("Horizontal");
    Debug.Log(horizontalInput);
    rb.velocity = new Vector2(horizontalInput * speed,rb.velocity.y);
   ```
9. 关键帧
10. 跳跃等
11. 动画状态机
12. GameObject.Find 无法找隐藏的物体
13. 创建 Animation Event 调用的函数方法实现爆炸效果。通过 Collider2D[] 获得范围内物体数组，实现炸开弹飞效果。
14. Collider2D[] aroundObject = Physics2D.OverlapCircleAll(transform.position,redius,targetLayer);
15. ForceMode2D.Force 力 持续 ForceMode2D.Impulse冲击力瞬间
16. item.GetComponent<Rigidbody2D>().AddForce((-pos+Vector3.up)*BombForce,ForceMode2D.Impulse);
17. Instantiate(bombPrefab, transform.position+new Vector3(Random.Range(-0.1f,0.1f),0,0), bombPrefab.transform.rotation);自己加了个range减少一个小bug
18. transform.position = Vector2.MoveTowards(transform.position, targetPoint.position,speed*Time.deltaTime);来回移动
19. transform.rotation = Quaternion.Euler(0f,0f,0f);反转
20. 继承 protected 父和子  virtual 虚method 子类可修改(override)
21. 了解在 Animator 窗口当中，使用多个 Layer 来控制管理多种动画状态。并且通过代码脚本来控制动画的切换。Finite States Machine 有限状态机 FSM
22. abstract 抽象类 Finite States Machine 有限状态机 FSM
23. enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("idel")
24. Hit point
25. 协程
      ```C++
      ```
26. alarmSign = transform.GetChild(0).gameObject;可以对隐藏的物体查找
27. targetPoint.SetParent(transform.parent.parent);查找2
28. FindObjectOfType<PlayerController>查找3
29. Sprite Filp 图片反转
30. 单例模式
   