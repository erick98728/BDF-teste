# HUD_SETUP (PC - Protótipo)

## 1) Criar Canvas
1. No Unity: `GameObject > UI > Canvas`.
2. Deixe `Render Mode = Screen Space - Overlay`.
3. Garanta que exista `EventSystem` na cena.

## 2) Criar objeto do HUD
1. Dentro do Canvas, crie um `Empty` chamado `HUDRoot`.
2. Adicione script `HUDController` em `HUDRoot`.

## 3) Criar textos obrigatórios
Dentro de `HUDRoot`, crie 4 textos (`UI > Text`):
- `HealthText`
- `DashStatusText`
- `EventMessageText`
- `EssenceText` (opcional)

Sugestão de posição:
- `HealthText`: topo esquerdo
- `DashStatusText`: abaixo da vida
- `EssenceText`: abaixo do dash
- `EventMessageText`: topo centro

## 4) Conectar no HUDController
No Inspector do `HUDController`:
- `Player Health`: arraste o componente `PlayerHealth` do Rubens.
- `Ability Manager`: arraste o `AbilityManager` do Rubens.
- `Health Text`: `HealthText`
- `Dash Status Text`: `DashStatusText`
- `Event Message Text`: `EventMessageText`
- `Essence Text`: `EssenceText` (opcional)
- `Message Duration`: 2.5 (ou valor desejado)

## 5) O que deve aparecer
- Vida: `Vida: atual/máxima`
- Dash: `Dash: BLOQUEADO` ou `Dash: DESBLOQUEADO`
- Mensagem temporária:
  - `Checkpoint ativado!`
  - `Memória recuperada! Dash desbloqueado.`
- Essência (placeholder): `Essência da Névoa: 0`

## 6) Observação
Se você não quiser texto de essência agora, deixe `Essence Text` vazio.
