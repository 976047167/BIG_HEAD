using BigHead.Net;
public enum MessageId : uint
{
    MIN = 0,
    NetworkConnect=1,
    NetworkDisconnect,

    /// <summary>
    /// UI编号从10000开始
    /// </summary>
    UI_START = 100000,
    /// <summary>
    /// UI窗口加载完成
    /// </summary>
    UI_FORM_LOADED,
    /// <summary>
    /// 创建角色
    /// </summary>
    UI_GAME_CREATE_CHARACTER,
    /// <summary>
    /// 游戏开始
    /// </summary>
    UI_GAME_START,
    /// <summary>
    /// 
    /// </summary>
    SCENE_LOAD_COMPLETE,
    /// <summary>
    /// 地图界面更新角色信息
    /// </summary>
    MAP_UPDATE_PLAYER_INFO,
    /// <summary>
    /// 地图界面更新角色经验
    /// </summary>
    MAP_UPDATE_PLAYER_EXP,

    /// <summary>
    /// 地图界面更新角色信息
    /// </summary>
    GAME_UPDATE_PLAYER_INFO,
    /// <summary>
    /// 地图界面更新角色经验
    /// </summary>
    GAME_UPDATE_PLAYER_EXP,



    /////<summary> 断线</summary>
    //MSG_LOST_CONNECT_TO_SERVER = 100000,

    /////<summary> 旧场景准备关闭（新场景准备加载）</summary>
    //MSG_SCENE_LOAD_PRE,
    /////<summary> 场景加载完成, 参数1：str 场景名</summary>
    //MSG_SCENE_LOAD_COMPLETE,
    /////<summary> 场景初始化完成</summary>
    //MSG_SCENE_INIT_COMPLETE,
    /////<summary>将进度条加载到100%</summary>
    //MSG_LOADING_TO_END,

    /////<summary> 主角复活  参数1：Player</summary>
    //MSG_PLAYER_REALIVE,
    /////<summary> 主角受伤  参数1：Player  参数2：血量  这个不对的呵呵</summary>
    //MSG_PLAYER_DAMAGE,
    /////<summary>更新主角信息界面，血量、真气 参数可为空，参数：Player</summary>
    //MSG_UPDATE_ROLE_INFO_PANEL,
    /////<summary> 怪物受伤  参数1：Monster 参数2：,</summary>
    //MSG_MONSTER_DAMAGE,
    /////<summary> 怪物霸体值  参数1：Monster 参数2：当前霸体值</summary>
    //MSG_MONSTER_BATICAST,

    /////<summary> 怪物离开视野（可能是死亡）  参数1：Monster</summary>
    //MSG_MONSTER_EXIT_VIEW,
    /////<summary> 怪物进入视野（可能是创建）  参数1：Monster</summary>
    //MSG_MONSTER_ENTER_VIEW,
    /////<summary> 怪物死亡  参数1：Monster</summary>
    //MSG_MONSTER_DEATH,
    /////<summary> 怪物创建  参数1：Monster</summary>
    //MSG_MONSTER_BORN,
    /////<summary>怪物开始释放某个技能   参数1：怪物actor 参数2：技能ID</summary>
    //MSG_MONSTER_RELEASE_SKILL,
    /////<summary> 怪物死亡后资源接管  参数1：Monster  </summary>
    //MSG_MONSTER_RES_OUT,


    /////<summary> 角色死亡  参数1：Player</summary>
    //MSG_PLAYER_DEATH,

    /////<summary> 技能冷却  参数1：冷却cd , </summary>
    //MSG_SKILL_ENTER_CD,
    /////<summary> 攻击到怪物  参数1：攻击到到怪物数           </summary>
    //MSG_SKILL_DAMAGED,
    /////<summary> 切换图标  参数1：按钮id  参数2：技能id</summary>
    //MSG_SKILL_UPDATEICON,
    /////<summary> 主UI准备好</summary>
    //MSG_SKILL_FIGHTUI_STANDBY,

    /////<summary> 物品主动飞向角色进行拾取  参数1：item</summary>
    //MSG_ITEM_FLY_PICK,
    /////<summary> 物品主动飞向角色到达  参数1：item</summary>
    //MSG_ITEM_FLY_PICK_ARRIVE,
    /////<summary> 主角属性的初始化</summary>
    //MSG_HERO_INIT_ATTR,

    ////技能

    /////<summary>　技能配置数据更新</summary>
    //MSG_SKILL_CONFIGDATA_CHANGE,
    /////<summary> 主角使用通用技能</summary>
    //MSG_SKILL_USE_COMMONSKILL,
    /////<summary> 主角使用通用技能的最后一个技能(只有怒气技能)</summary>
    //MSG_SKILL_USE_LAST_COMMONSKILL,
    /////<summary> 主角使用普通技能         </summary>
    //MSG_SKILL_USE_NORMALSKILL,
    /////<summary> 通过界面使用技能    </summary>
    //MSG_SKILL_USE_FROMUI,
    /////<summary> 使用特殊技能时，角色碰撞到其它角色          </summary>
    //MSG_SKILL_ACTOR_CONLLISION_OBJ,
    /////<summary>技能击中某角,</summary>
    //MSG_SKILL_HIT_ACTOR,
    /////<summary> 主角数据更新</summary>
    //MSG_PLAYER_ATTR_UPDATE,
    /////<summary>装备数据转技能效果 </summary>
    //MSG_EQUIP_PERF_CTRL_INIT,
    /////<summary>状态发生改变 参数1：状态index，参数2：bool值</summary>
    //MSG_SKILL_STATE_CHANGE,



    ////buff

    /////<summary> 主角buff禁止移动</summary>
    //MSG_BUFF_PLAYER_DISABLE_MOVE,
    /////<summary> 主角buff解除禁止移动 </summary>
    //MSG_BUFF_PLAYER_ENABLE_MOVE,
    /////<summary> 主角buff解除禁止移动 </summary>
    //MSG_BUFF_PLAYER_SORT,
    /////<summary> buff变化通知</summary>
    //MSG_BUFF_CHANGE,

    //// 关卡事件

    /////<summary> 怪群触发器等待激活  参数1：怪群ID</summary>
    //MSG_LEVEL_TRIGGER_READY,
    /////<summary> 怪群触发器已激活  参数1：怪群组ID, 参数2：怪群ID, 参数3:第几次触发</summary>
    //MSG_LEVEL_TRIGGER_DONE,
    /////<summary> 怪群触发器已激活  参数1：怪群组ID, 参数2：怪群ID, 参数3:第几次触发(传递给脚本）</summary>
    //MSG_LEVEL_TRIGGER_DONE_SCRIPT,
    /////<summary> 怪物触发器已创建</summary>
    //MSG_LEVEL_CREATE_NEXT_TRIGGER,
    /////<summary>单人关卡里所有怪数据都知道 参数1：</summary>
    //MSG_LEVEL_SINGLE_ALL_MONSTER_KNOWED,
    /////<summary> 创建了一波怪ALL_MONSTER_KNOW,</summary>
    //MSG_LEVEL_CREATE_BATCH,

    //// 触屏点击事件

    //MSG_CLICK_HAPPEN,
    /////<summary> 角色被点了  参数1：IActor</summary>
    //MSG_CLICK_PLAYER,
    /////<summary> Npc被点中了 参数1：Transform</summary>
    //MSG_CLICK_NPC,
    /////<summary> 点中了功能节点  参数1：Transform</summary>
    //MSG_CLICK_FUNCTION_OBJECT,
    /////<summary> 点中了其它节点  参数1：Transform</summary>
    //MSG_CLICK_OBJECT,
    /////<summary>点中了采集怪 参数1：Transform</summary>
    //MSG_CLICK_PICK_OBJ,
    /////<summary> 什么都没点中  </summary>
    //MSG_CLICK_NOTHING,

    //// 网络相关事件

    /////<summary>　注销协议返回成功</summary>
    //MSG_NETWORK_LOGOUT,

    //// 技能状态事件

    /////<summary> 成功释放技能 参数1：m_actor 参数2：技能ID</summary>
    //MSG_CAST_SPELL_SUCCESS,
    /////<summary> 技能状态启动 参数1:CasterIActor, 参数1:IActor, 参数2:SkillProperty</summary>
    //MSG_ACT_STATE_START,
    /////<summary> 服务器控制的技能 参数1：caster actor, 参数2： skill id</summary>
    //MSG_SRV_CAST_NPC_SKILL,

    /////<summary> 角色属性计算刷新</summary>
    //MSG_REFRESH_CHARACTER_ATTR,

    /////<summary> 打开关卡选择</summary>
    //MSG_OPEN_LEVELSEL_PANEL,

    /////<summary>创建场景跳转点，参数1：跳转物体，参数2：跳转ID</summary>
    //MSG_CREATE_SCENE_SWITCH,

    /////<summary>摇杆移动事件开始触发</summary>
    //MSG_JIGGLE_BEGIN_MOVE,

    /////<summary>摇杆停止移动事件</summary>
    //MSG_JIGGLE_STOP_MOVE,

    /////<summary>怪物开始警戒，参数1：警戒目标  参数2：monster </summary>
    //MSG_MONSTER_AI_GUARD,

    //// 过场触发

    /////<summary>过场动画触发，参数： index 过场动画文件索引号 </summary>
    //MSG_SCENARIO_TRIGGER,
    /////<summary>过场动画结束</summary>
    //MSG_SCENARIO_STOP,
    /////<summary>隐藏显示战斗ui 参数1: bool值，隐藏还是显示</summary>
    //MSG_SWICH_SHOW_HIDE_FIGHT_UI,
    /////<summary>隐藏副本内导航 参数1: bool值，隐藏还是显示   </summary>
    //MSG_SWICH_SHOW_HIDE_PRTHFIDING,
    /////<summary>过场动画事件交互，参数：int 事件id,实时动画系统发送给外部系统THFIDI,</summary>
    //MSG_SCENARIO_SEND_EVENT,
    /////<summary>外部系统向实时动画系统发送事件</summary>
    //MSG_SEND_TO_SCENARIO_EVENT,
    //MSG_RESUME_SCENARIO_ACTION,
    /////<summary>AI中触发实时动画的消息, 参数：monsterID,actionType,param,和表对应I,</summary>
    //MSG_SCENARIO_AI_TRIGGER,

    /////<summary>用实时资源替换动画里的资源, 参数1：id,参数2：之前的资源名</summary>
    //MSG_USEQUENCE_REPLACE_RES,
    /////<summary>实时动画暂停或停止, 参数：1表示停止，2表示暂停; </summary>
    //MSG_USEQUENCE_STOP_PAUSE,
    /////<summary>剧情动画，UI的显示与隐藏</summary>
    //MSG_USEQUENCE_UI_SHOW_HIDE,
    /////<summary>剧情动画，Hero的显示与隐藏</summary>
    //MSG_USEQUENCE_HERO_SHOW_HIDE,
    /////<summary>剧情动画，ActorEffect挂载</summary>
    //MSG_USEQUENCE_ACTOR_EFFECT_SHOW,


    /////<summary>停止移动摇动产生的移动</summary>
    //MSG_STOP_JIGGLE_CONTROL_MOVE_EVENT,

    /////<summary>身上的属性发生变化</summary>

    /////<summary>身上的属性发生了变化 参数1：uikey，参数2：uiFrom，参数3：uiTo,参数4：int64 actorid</summary>
    //MSG_ATTR_CHANGE,

    /////<summary>情景对话</summary>

    //MSG_NPC_INTER_DIALOGUE_CLOSE,

    /////<summary> 加载完level，并且隐藏了loading界面</summary>

    //MSG_HIDE_LEVEL_LOADING_COMPLETE,

    /////<summary>销毁版本更新UI</summary>

    //MSG_DESTROY_VERSION_UPDATE_UI,

    /////<summary>--屏幕溅血效果</summary>
    //MSG_SPLASH_BLOOD,
    /////<summary>--屏幕破碎效果</summary>
    //MSG_BROKEN_SCREEN,
    //MSG_PLAYER_OPERATE,
    /////<summary>--显示闪屏效果</summary>
    //MSG_SHOW_FLASH_SCREEN,
    /////<summary>--关闭闪屏效果</summary>
    //MSG_CLOSE_FLASH_SCREEN,

    /////<summary>刷新网络状态UI</summary>

    //MSG_Refresh_NET_STATE_UI,

    ////特殊挑战相关

    /////<summary>释放了技能 </summary>
    //MSG_NOTIFY_SKILL_USE,
    /////<summary>怪物霸体下死亡</summary>
    //MSG_MONSTER_DEATH_INBATI,
    /////<summary>一个判定帧杀死了规定数量的怪物</summary>
    //MSG_KILLED_MONTERS_INFRAME,
    /////<summary>主角被技能击中了TERS_INFRA,</summary>
    //MSG_HIT_BY_SKILL,
    /////<summary>怪物buff下死亡</summary>
    //MSG_MONSTER_DEATH_INBUFF,

    ////怒气相关

    /////<summary>增加怒气了 参数一：技能id，参数二：是否触发会心一击,参数三：是否是被攻击</summary>
    //MSG_ADD_ANGER_VALUE,
    /////<summary>怒气技能开始和借宿 参数:bool型，怒气技能开始为true，结束为false</summary>
    //MSG_USE_ANGER_SKILL,
    /////<summary>技能UI，怒气装备发生改变时执行</summary>
    //MSG_REFRESH_ANGER_SKILL_DATA,

    ////血条相关

    /////<summary>使用了怒气技能</summary>
    //MSG_REFRESSH_PLAYER_HUD_UI,

    ////野外

    /////<summary>安全区进出 参数1：actor</summary>
    //MSG_ENTER_SAFE_AREA,
    /////<summary>安全区进出 参数1：actor  </summary>
    //MSG_EXIT_SAFE_AREA,

    ////轻功

    /////<summary>轻功 参数1：actor 参数2:状态true\false</summary>

    /////<summary>采集MSG_DODGE_STATE ,</summary>

    /////<summary>采集开始</summary>
    //MSG_GATHER_STATE_BEGIN,
    /////<summary>采集失败 </summary>
    //MSG_GATHER_STATE_FAIL,
    /////<summary>采集成功</summary>
    //MSG_GATHER_STATE_SUCCESS,


    ////云娃语音

    /////<summary>玩家登录云娃结果</summary>
    //YUNVA_LOGIN_RES,
    /////<summary>玩家登出云娃</summary>
    //YUNVA_LOGOUT_RES,
    /////<summary>玩家登录频道结果</summary>
    //YUNVA_CHANNEL_LOGIN_RES,
    /////<summary>玩家登出频道结果</summary>
    //YUNVA_CHANNEL_LOGOUT_RES,
    /////<summary>频道消息通知</summary>
    //YUNVA_CHANNEL_MESSAGE_NOTIFY,
    /////<summary>玩家私聊通知TI,</summary>
    //YUNVA_CHAT_FRIEND_NOTIFY,
    /////<summary>录音结果TI,</summary>
    //YUNVA_RECORD_RESULT,
    /////<summary>语音识别结果</summary>
    //YUNVA_SPEECH_RECOGNIZE_RES,
    /////<summary>发送频道语音消息结果</summary>
    //YUNVA_SEND_CHANNEL_VOICE_RES,
    /////<summary>发送频道文本消息结果</summary>
    //YUNVA_SEND_CHANNEL_TEXT_RES,
    /////<summary>发送私聊语音消息结果,</summary>
    //YUNVA_SEND_P2P_VOICE_RES,
    /////<summary>发送私聊文本消息结果</summary>
    //YUNVA_SEND_P2P_TEXT_RES,
    /////<summary>录音时音量变化</summary>
    //YUNVA_RECORD_VOLUME_NOTIFY,
    ////采集，钓鱼

    /////<summary>进入采集怪触发区域 参数1：bool 是否是私有怪，参数2： guid， 参数3：configID</summary>
    //MSG_ENTER_PICK_TRIGGER,
    /////<summary>离开采集怪触发区域 参数1：bool 是否是私有怪，参数2： guid， 参数3：configID  </summary>
    //MSG_EXIT_PICK_TRIGGER,
    /////<summary>进入鱼塘范围 参数：鱼塘index</summary>
    //MSG_ENTER_FISHING_TRIGGER,
    /////<summary>离开鱼塘范围 参数：鱼塘index  </summary>
    //MSG_EXIT_FISHING_TRIGGER,
    /////<summary>采集怪死亡  </summary>
    //MSG_PICK_MONSTER_DEATH,

    ////头顶气泡

    /////<summary>怪物头顶气泡</summary>
    //MSG_MONSTER_SPEECH,

    ////骑乘相关

    /////<summary> 成功上马  参数1：Player</summary>
    //MSG_GET_ON_RIDE,
    /////<summary> 成功下马  参数1：Player</summary>
    //MSG_GET_OFF_RIDE,
    /////<summary>通知server主角下马</summary>
    //MSG_GET_OFF_RIDE_NEED_SEND_SER,
    /////<summary>骑马按钮变灰RIDE_NEED_SEND_S,</summary>
    //MSG_RIDE_BTN_ENABLE,
    /////<summary>骑马按钮变灰</summary>
    //MSG_RIDE_BTN_DISABLE,

    /////<summary>主角移动状态 参数：bool 是否在移动</summary>
    //MSG_HERO_MOVE_EVENT,
}