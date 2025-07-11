# SpartaCodingClub
'내일배움캠프' TEXT RPG

# 🛡️ TEXT RPG - Sparta Village Adventure

**콘솔 기반의 텍스트 RPG 게임**입니다.  
플레이어는 '스파르타 마을'에 도착해 이름과 직업을 정하고, 다양한 활동(상태 보기, 인벤토리, 상점)을 통해 캐릭터를 성장시켜 나갑니다.

---

## 📜 게임 소개
- 이름과 직업을 직접 설정
- 능력치 기반 캐릭터 시스템
- 아이템 구매 및 장착 관리
- 콘솔 인터페이스로 텍스트 중심 플레이

---

## 🎮 주요 기능

| 기능           | 설명 |
|----------------|------|
| 🔍 상태 보기     | 현재 캐릭터의 능력치와 정보 확인 |
| 🎒 인벤토리     | 아이템 목록 조회 및 장착/해제 |
| 🛒 상점         | 아이템 구매, 아이템 판매, 골드 관리 |
| 🎭 직업 선택     | 전사 / 도적 선택 가능 |
| 🏥 휴식하기 선택     | 휴식하기 기능을 이용하여 체력 회복 |
| 📝 사용자 입력   | 선택지 기반 진행 (번호 입력 방식) |

---

## 🛠️ 설치 및 실행 방법

1. C#이 실행 가능한 개발 환경 준비 (예: Visual Studio, VS Code)
2. `.cs` 파일을 프로젝트에 추가
3. `Program.cs` 또는 `Main()` 함수에서 실행

--- 
## 🎮 게임 플레이 스크린샷
<img width="448" height="264" alt="Image" src="https://github.com/user-attachments/assets/f4b6ceef-22e4-4c09-bc02-3899fd9fd487" /> <img width="639" height="320" alt="Image" src="https://github.com/user-attachments/assets/d6dd6646-0a4b-4f15-a2f8-a6ed18177e79" />

---
```bash
dotnet run

/TextRPG
 ├─ Program.cs         # Main 함수
 ├─ GameManager.cs     # 게임 흐름 관리
 ├─ Player.cs          # 캐릭터 정보
 ├─ Inventory.cs       # 인벤토리 시스템
 ├─ Shop.cs            # 상점 시스템
 └─ Item.cs            # 아이템 정보

