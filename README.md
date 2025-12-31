# EndlessRunner

## Description
此工程用来进行性能测试，功能测试可使用 [EndlessRunner_beta](https://gitlab-u3d.internal.unity.cn/jilong.cai/endlessrunner_beta "GitLab Repository")
 - main: 主分支，performance分支稳定后会merge进main分支
 - performance: 性能测试打包使用此分支，主要改动在此分支上进行，CDN使用的Bucket为性能测试使用(例如`1.5.0`)
 - ~~dev: 工程的改动和修改先在此branch上进行，CDN使用的Bucket为`dev_branch`~~

  ⚠️ 功能测试请在main分支上进行，performance分支主要用来进行微信小游戏性能测试打包

## Changelogs

### 20250411
Changed:
 - Update Editor to Tuanjie 1.5.0

### 20250414
Add:
 - Create dev branch

Changed:
 - Clear AutoStreaming UOS CDN Settings

 ### 20250416
 Add:
  - Create performance branch

Changed:
 - change profile
 - Update README