# BOSQUE_ROOM_SETUP

## Objetivo
Montar uma sala pequena e jogavel do Bosque da Nevoa Perdida para validar movimentacao, camera, combate, dano, checkpoint, respawn e bloqueio por Dash com placeholders.

Nao monte Lucarelli nesta cena ainda. Reserve apenas uma area final que possa virar a entrada da arena depois.

## Cena recomendada
1. Crie uma cena Unity chamada `BosquePrototypeRoom`.
2. Salve em `Assets/_Project/Scenes`.
3. Use quadrados, sprites basicos ou objetos vazios com gizmos como placeholders.
4. Mantenha a cena pequena, aproximadamente uma tela e meia de largura para o primeiro teste.

## Layout sugerido
Use este fluxo da esquerda para a direita:

```text
[Spawn Rubens] -- piso inicial -- [Inimigo 01] -- plataforma baixa
          -- [Checkpoint] -- [Inimigo 02] -- [Dash Gate]
          -- plataforma/queda curta -- [Final_Lucarelli_Entry]
```

Uma versao simples em coordenadas aproximadas:

| Objeto | Posicao aproximada | Escala aproximada |
| --- | --- | --- |
| `Ground_Main` | `(0, -2, 0)` | `(24, 1, 1)` |
| `Platform_Low` | `(-2, 0, 0)` | `(4, 0.5, 1)` |
| `Platform_AfterGate` | `(9, -0.5, 0)` | `(3, 0.5, 1)` |
| `Rubens_Player` | `(-9, -0.8, 0)` | conforme placeholder |
| `Enemy_01` | `(-5, -0.8, 0)` | conforme placeholder |
| `Checkpoint_01` | `(0, -0.8, 0)` | area de trigger |
| `Enemy_02` | `(3, -0.8, 0)` | conforme placeholder |
| `DashGate_01` | `(6, -0.5, 0)` | alto o bastante para bloquear |
| `Final_Lucarelli_Entry` | `(10, -0.8, 0)` | marcador vazio |

Adicione `Enemy_03` em uma plataforma se quiser testar ataque aereo e camera, mas dois inimigos ja bastam para o fluxo minimo.

## Hierarquia sugerida
```text
BosquePrototypeRoom
|-- Systems
|   |-- GameManager
|-- Player
|   |-- Rubens_Player
|       |-- GroundCheck
|       |-- AttackPoint
|-- Camera
|   |-- Main Camera
|-- Level
|   |-- Ground_Main
|   |-- Platform_Low
|   |-- Platform_AfterGate
|   |-- Final_Lucarelli_Entry
|-- Enemies
|   |-- Enemy_01
|   |-- Enemy_02
|-- World
|   |-- Checkpoint_01
|       |-- RespawnPoint
|-- Gates
    |-- DashGate_01
```

## Layers e Tags
Crie estas Layers em `Project Settings > Tags and Layers`:
- `Ground`: pisos e plataformas que liberam o pulo.
- `Enemy`: inimigos e colliders que a katana deve encontrar.

Tags nao sao obrigatorias para esta sala. Os scripts atuais localizam Rubens por componentes como `PlayerHealth` e `AbilityManager`.

## Sistemas da cena
### `GameManager`
1. Crie um GameObject vazio chamado `GameManager` dentro de `Systems`.
2. Adicione o script `GameManager`.
3. Mantenha `Respawn Delay` curto para o prototipo.

Sem esse objeto, checkpoint e respawn nao conseguem fechar o fluxo completo.

## Montar `Rubens_Player`
1. Crie `Rubens_Player` dentro de `Player`.
2. Adicione um `SpriteRenderer` placeholder para enxergar o corpo.
3. Adicione estes componentes:
   - `Rigidbody2D`.
   - `CapsuleCollider2D` ou `BoxCollider2D`.
   - `PlayerController2D`.
   - `PlayerHealth`.
   - `PlayerCombat`.
   - `AbilityManager`.
4. No `Rigidbody2D`:
   - use `Body Type = Dynamic`;
   - mantenha `Simulated` ligado;
   - use gravidade maior que zero;
   - congele `Rotation Z`.
5. No collider do corpo:
   - deixe `Is Trigger` desligado;
   - ajuste ao volume do placeholder.

### `GroundCheck`
1. Crie um filho chamado `GroundCheck`.
2. Posicione no centro dos pes, levemente abaixo do collider.
3. Arraste para `PlayerController2D > Ground Check`.
4. Marque a Layer `Ground` em `PlayerController2D > Ground Layer`.

### `AttackPoint`
1. Crie um filho chamado `AttackPoint`.
2. Posicione a frente de Rubens, na altura do ataque curto da katana.
3. Arraste para `PlayerCombat > Attack Point`.
4. Em `PlayerCombat > Enemy Layers`, marque `Enemy`.
5. Comece com o raio de ataque padrao e ajuste pelo gizmo vermelho.

### Dash na sala
1. Deixe `AbilityManager > Dash Unlocked` desmarcado no inicio do teste.
2. No `PlayerController2D`, mantenha ou ajuste:
   - `Dash Key`;
   - `Dash Speed`;
   - `Dash Duration`;
   - `Dash Cooldown`.
3. Para teste temporario, use o menu de contexto do `AbilityManager` e execute `Debug/Unlock Dash`.

## Configurar camera
1. Use a `Main Camera` da cena.
2. Adicione `CameraFollow2D`.
3. Arraste `Rubens_Player` para `Target`.
4. Ajuste `Offset` para manter Rubens e plataformas proximas no enquadramento.
5. Mantenha `Smooth Time` curto o bastante para o teste de plataforma.

## Chao e plataformas
Para cada objeto de piso:
1. Crie um sprite quadrado ou placeholder visual.
2. Adicione `BoxCollider2D`.
3. Deixe `Is Trigger` desligado.
4. Atribua a Layer `Ground`.

Comece com:
- `Ground_Main`: piso largo para spawn, combate e checkpoint.
- `Platform_Low`: plataforma que exige pulo simples.
- `Platform_AfterGate`: plataforma ou piso curto depois da barreira.

## Inimigos basicos
Monte `Enemy_01` e `Enemy_02` assim:
1. Crie um GameObject com sprite placeholder.
2. Atribua a Layer `Enemy`.
3. Adicione:
   - `Rigidbody2D`.
   - `BoxCollider2D` ou `CapsuleCollider2D`.
   - `BasicPatrolEnemy`.
4. O `BasicPatrolEnemy` herda `EnemyBase`, entao nao e necessario adicionar `EnemyBase` separadamente.
5. No `Rigidbody2D`:
   - use `Body Type = Dynamic`;
   - deixe gravidade ativa;
   - congele `Rotation Z`.
6. No collider:
   - deixe `Is Trigger` desligado para o teste inicial.
7. Ajuste no Inspector:
   - `Max Health` e `Current Health`;
   - `Move Speed`;
   - `Patrol Distance`;
   - `Contact Damage`;
   - `Damage Interval`.

Posicione patrulhas pequenas para o inimigo continuar sobre o piso placeholder. O script nao detecta beirada ainda.

## Checkpoint
1. Crie `Checkpoint_01` dentro de `World`.
2. Adicione um placeholder visual simples, se quiser.
3. Adicione:
   - `BoxCollider2D`;
   - `Checkpoint`.
4. Marque `BoxCollider2D > Is Trigger`.
5. Ajuste o volume para Rubens atravessar a area.
6. Crie um filho chamado `RespawnPoint`.
7. Posicione `RespawnPoint` acima do piso, fora do corpo de inimigos.
8. Arraste esse filho para `Checkpoint > Respawn Point`.

Ao atravessar o trigger, o Console deve registrar a ativacao e o `GameManager` passa a usar esse ponto para respawn.

## Barreira por Dash
1. Crie `DashGate_01` dentro de `Gates`.
2. Use um sprite retangular placeholder que feche a passagem.
3. Adicione:
   - `BoxCollider2D`;
   - `AbilityGate`.
4. Deixe o collider solido, com `Is Trigger` desligado.
5. Em `AbilityGate`:
   - mantenha `Required Ability = Dash`;
   - arraste o `BoxCollider2D` para `Blocking Collider`;
   - opcionalmente arraste o visual da barreira para `Locked Visual`;
   - arraste o `AbilityManager` de Rubens para o campo `Ability Manager` para a abertura reagir imediatamente ao unlock.

Antes do Dash, Rubens deve colidir com essa barreira e o Console deve registrar que falta a habilidade. Depois de `UnlockDash()`, o collider abre a passagem.

## Area final para Lucarelli
1. Crie um GameObject vazio chamado `Final_Lucarelli_Entry`.
2. Posicione depois da barreira.
3. Use um sprite placeholder, texto no Scene view ou um objeto visual simples para marcar o fim da sala.
4. Nao adicione chefe, cutscene ou transicao de cena nesta etapa.

Esse marcador serve para reservar o ponto onde a proxima entrega pode conectar a arena de Lucarelli.

## Checklist de conexoes
Antes de apertar Play, confira:
- Existe `GameManager` na cena.
- `Rubens_Player` tem movimento, vida, combate e `AbilityManager`.
- `GroundCheck` esta ligado ao `PlayerController2D`.
- `Ground Layer` do controller inclui `Ground`.
- `AttackPoint` esta ligado ao `PlayerCombat`.
- `Enemy Layers` do combate inclui `Enemy`.
- Pisos e plataformas usam Layer `Ground`.
- Inimigos usam Layer `Enemy`.
- `Main Camera > CameraFollow2D > Target` aponta para Rubens.
- Checkpoint usa collider trigger e tem `RespawnPoint` seguro.
- Gate usa collider solido e exige `Dash`.

## Fluxo de teste no Play Mode
1. Inicie com Rubens no spawn esquerdo.
2. Ande com `A`/`D` ou setas e pule com `Space`.
3. Ataque `Enemy_01` com a tecla configurada em `PlayerCombat`.
4. Encoste em um inimigo e confira dano e invulnerabilidade de Rubens pelos logs.
5. Atravesse `Checkpoint_01`.
6. Mate Rubens por dano ou usando `PlayerHealth.Die()` em teste temporario.
7. Confirme que ele respawna no checkpoint com vida restaurada.
8. Chegue em `DashGate_01` ainda sem Dash e confirme o bloqueio.
9. No `AbilityManager`, use `Debug/Unlock Dash`.
10. Atravesse a barreira e chegue ao marcador `Final_Lucarelli_Entry`.

## Problemas comuns
- Rubens nao pula: confira Layer `Ground`, `GroundCheck` e colliders do piso.
- Katana nao acerta: confira Layer `Enemy`, `Enemy Layers`, collider do inimigo e posicao do `AttackPoint`.
- Checkpoint nao ativa: confira `Is Trigger`, `GameManager` na cena e `PlayerHealth` em Rubens.
- Gate nao abre ao unlock: ligue o `AbilityManager` de Rubens no gate ou toque a barreira novamente depois do unlock.
- Inimigo cai da plataforma: diminua `Patrol Distance` ou use piso mais largo.

## Placeholders desta sala
- Sprites de Rubens, inimigos, checkpoint, barreira e piso.
- Layout do Bosque sem arte final, parallax ou atmosfera visual.
- Marcador final sem arena de chefe.
- Logs no Console no lugar de feedback de UI e efeitos.

## Proximo passo
Depois que este fluxo estiver confiavel, a area final pode receber a primeira versao de Lucarelli, a logica de derrota e a recompensa que chama `AbilityManager.UnlockDash()`.
