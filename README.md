

#Windows Course Project
#2015年同济大学Windows期末课程项目设计
###Hello Money——一款轻便易用的Windows平台记账工具


## 项目规划

###已经实现的功能

- 添加支出或者收入
- 展示今日数据
- 月报表（年报表与月报表内容几乎一样，涉嫌重复）
- 查询功能（可以通过时间段进行查询，也可以通过关键字查询）
- 柱状图和饼状图展示（现在是把所有支出和收入放到一个表里面了）

###接下来的任务

- 添加和完善功能
    - 完善entity,可以考虑重写实体类，可以考虑是否通过HTTP请求进行数据备份或者恢复。也可以通过微软onedrive来进行：
        - [one drive sdk home page](https://dev.onedrive.com/sdks.htm)

        - [one drive doc](https://msdn.microsoft.com/library/windows/apps/br207847?cs-save-lang=1&cs-lang=csharp#code-snippet-1)

        - [one drive cs sdk](https://github.com/onedrive/onedrive-sdk-csharp)

    - 收入支出分类可以用户定义
        - 现有的支出
            - 房租
            - 娱乐
            - 餐饮
            - 交通
            - 其它
        - 收入
            - 工资
            - 股票
            - 投资
            - 其它
    - 分开明细，现金？银行卡？支付宝？（可扩展）
    - 细化图表分析
        - 收入和支出是否应该分开按照类别进行绘图分析？
        - 是否应该对支出或者收入画折线图预测走势？
        - 开源库（非原先的）
            - [Live-Charts](https://github.com/beto-rodriguez/Live-Charts)

    - 定时提醒记账功能
    - 预算？
    - 是否接入语音SDK？不过能用语音干啥？（系统有API可以用！）
        - 可以说：『帮我打开巴拉巴拉』
        - 可以说：『我刚刚巴拉巴拉花了￥￥￥』
        - 可以说：『我刚刚巴拉巴拉赚了$$$』
        - 也可以放到description中
        - 。。。无限遐想中
    - 账本分类？
    - 接一些理财的页面做理财推荐？太扯了
- UI界面重构，界面元素设计
    - 主页放些什么？
    - 设计哪几个界面？
    - 逻辑上怎么连接各个页面？


##开发环境：
* <strong>Windows OS(Parallel Desktop)</strong>
* <strong>Visual studio 2015 </strong>



## 使用方法

## 基本功能





##Members:
<strong><a href="https://github.com/Yiiinsh">鄞韶涵</a>,
<strong><a href="https://github.com/yue9944882">金敏</a>,
<strong><a href="https://github.com/HermanZzz">张航</a>
