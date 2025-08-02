
# MCP Todo 워크숍 전체 단계별 기록 및 결과

## 00: 개발 환경 세팅
- .NET 9, VS Code, Azure CLI, Docker 등 필수 개발 도구 설치 및 환경 구성

## 01: Monkey App 개발 (MCP 프로토콜 적용)
- MCP 기반 콘솔 앱 개발
- 원숭이 데이터 관리 기능 구현 (조회, 검색, 랜덤 선택 등)
- 터미널 출력 예시:
  ![Monkey App Terminal Output 1](./images/monkey-app-1.png)
  ![Monkey App Terminal Output 2](./images/monkey-app-2.png)
  ![Monkey App Terminal Output 3](./images/monkey-app-3.png)
  ![Monkey App Terminal Output 4](./images/monkey-app-4.png)

## 02: MCP 서버 개발 (할 일 관리)
- ASP.NET Core Minimal API로 MCP 서버 구현
- Entity Framework Core + SQLite 인메모리 DB 연동
- MCP 툴 등록 및 엔드포인트 구성
- 주요 테스트 시나리오:
  1. **초기 할 일 목록 조회**: 비어 있음
  2. **"lunch at 12pm" 추가**: 정상적으로 추가됨
  3. **"lunch at 12pm" 완료 처리**: 완료 상태로 변경됨
  4. **"meeting at 3pm" 추가**: 정상적으로 추가됨
  5. **회의 시간을 3:30pm으로 변경**: 정상적으로 수정됨
  6. **회의 취소(삭제)**: 정상적으로 삭제됨
- 최종 할 일 목록: lunch at 12pm (완료)

## 03: MCP 서버 원격 배포
- MCP 서버를 Azure Container Apps에 배포
- 원격 MCP 엔드포인트(`https://mcptodoserver-containerapp.ashyrock-5b0c11f2.koreacentral.azurecontainerapps.io/mcp`)로 연결
- `.vscode/mcp.json`에 엔드포인트 설정

## 04: MCP 클라이언트 개발 (Blazor 웹앱)
- Blazor 기반 MCP Todo Client 웹앱 개발
- MCP 서버와 연동하여 실시간 할 일 관리
- 웹 UI 예시:
  ![MCP Todo Client Web UI](./images/mcp-todo-client.png)

---

테스트 결과, MCP 서버 및 클라이언트의 모든 주요 기능이 정상적으로 동작함을 확인했습니다.
