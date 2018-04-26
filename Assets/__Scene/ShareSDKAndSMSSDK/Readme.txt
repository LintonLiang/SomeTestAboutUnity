场景IInit -> login -> info -> play

主要使用了ShareSDK和SMSSDK的一些东西
1.第三方登录（微博平台，且用的测试模式，只有特定的测试账号可使用，正式使用时得等过审核）
2.获取指定平台账号的各种信息（需先登录，所以也是微博平台做示例，信息写成文件存在本地Application.persistentDataPath下，安卓目录Android->com.Linton.ShareSDKDemo->file）
3.各个平台的分享（各个平台需要单独记录信息的，需在对应平台的进行注册）
4.SMSSDK只有短信验证功能，其他功能官方表示大多已经弃用或者不再维护了，慎重使用