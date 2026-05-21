# PAUSE_MENU_SETUP (PC - Protótipo)

## 1) Criar painel de pausa
1. No Canvas da cena, crie um `Panel` chamado `PausePanel`.
2. Deixe o `PausePanel` inicialmente **desativado**.
3. Dentro do painel, crie um texto simples: `Jogo Pausado`.

## 2) Criar botões obrigatórios
No `PausePanel`, crie três botões (`UI > Button`):
- `Btn_Continuar`
- `Btn_Reiniciar`
- `Btn_Sair`

Renomeie os labels para:
- Continuar
- Reiniciar Cena
- Sair

## 3) Adicionar controlador
1. Crie objeto `PauseMenu` na cena (ou use `HUDRoot`).
2. Adicione script `PauseMenuController`.
3. No Inspector, configure:
   - `Pause Key = Escape`
   - `Pause Panel = PausePanel`
   - `Main Menu Scene Name` opcional (ex.: `MainMenu`)

## 4) Conectar botões no OnClick
No `Button -> OnClick`:
- `Btn_Continuar` -> `PauseMenuController.ContinueGame()`
- `Btn_Reiniciar` -> `PauseMenuController.RestartScene()`
- `Btn_Sair` -> `PauseMenuController.ExitOrMainMenu()`

## 5) Comportamento esperado
- `ESC` pausa/despausa.
- Ao pausar: `Time.timeScale = 0` e painel aparece.
- Continuar: volta para `Time.timeScale = 1`.
- Reiniciar: recarrega cena atual.
- Sair:
  - se houver `MainMenu` válido, carrega menu;
  - sem menu: no Editor mostra log; no build fecha jogo.
