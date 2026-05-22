# PROTOTYPE_REVIEW

## Objetivo
Registrar a revisao do prototipo atual de `Tester` sem ampliar o escopo alem do Bosque da Nevoa Perdida.

## Estado revisado
O codigo do fluxo principal esta separado por responsabilidade:
- `Player`: movimento, vida e habilidades de Rubens.
- `Combat`: ataque da katana e contrato `IDamageable`.
- `Enemies`: vida e patrulha do inimigo basico.
- `Bosses`: vida de chefe e comportamento inicial de Lucarelli.
- `World`: checkpoint.
- `Interactables`: bloqueio por habilidade.
- `Core`: camera e respawn.
- `UI`: HUD e pausa.

As pastas de projeto para `Scenes`, `Prefabs`, `Sprites`, `Audio`, `Materials`, `Animations` e `ScriptableObjects` ja existem. Nesta revisao, `Scenes` e `Prefabs` ainda contem apenas placeholders de pasta, entao a sala jogavel depende da montagem no Editor descrita nos guias.

## Sistemas implementados
### Rubens
- Movimento horizontal por `Input.GetAxisRaw("Horizontal")`.
- Pulo condicionado ao `GroundCheck` e `Ground Layer`.
- Flip horizontal pela direcao de movimento.
- Dash bloqueavel controlado por `AbilityManager`.
- Vida maxima, vida atual, cura, dano, invulnerabilidade, morte e reset para respawn.

### Combate
- Ataque de katana curto por `PlayerCombat`.
- `AttackPoint`, raio de ataque, cooldown, dano e `Enemy Layers` configuraveis.
- Contrato `IDamageable` consumido por inimigo basico e chefe.
- Gizmo da area de ataque.

### Inimigo e chefe
- `EnemyBase` com vida, dano recebido e morte.
- `BasicPatrolEnemy` com patrulha horizontal e dano por contato.
- `BossBase` com vida e evento de derrota.
- `LucarelliBoss` com avanco horizontal e ataque curto em area.
- Derrota de Lucarelli chama `AbilityManager.UnlockDash()`.

### Mundo e progressao
- Checkpoint por `Collider2D` trigger.
- `GameManager` registra posicao de respawn e restaura Rubens.
- `AbilityGate` bloqueia passagem por habilidade e abre ao receber Dash.
- Layers `Ground` e `Enemy` estao reservadas no projeto para o fluxo basico.

### UI de teste
- HUD em Canvas com vida, estado de Dash e mensagens temporarias.
- Mensagens para dano, morte, checkpoint, respawn, Dash e gate bloqueado.
- Menu de pausa simples com `ESC`, continuar, reiniciar cena e sair em build.

## Fluxo de teste do prototipo
1. Monte ou abra uma sala de Bosque seguindo `BOSQUE_ROOM_SETUP.md`.
2. Inicie com `AbilityManager > Dash Unlocked` desmarcado.
3. Ande com `A`/`D` ou setas.
4. Pule com `Space` sobre pisos na Layer `Ground`.
5. Ataque um inimigo basico com a tecla configurada em `PlayerCombat`.
6. Confirme que o inimigo recebe dano e e destruido ao zerar vida.
7. Encoste em outro inimigo para Rubens receber dano e o HUD atualizar vida.
8. Atravesse o checkpoint e confirme a mensagem de ativacao.
9. Mate Rubens por dano ou pelo menu de contexto de `PlayerHealth` durante teste.
10. Confirme respawn no ultimo checkpoint, vida restaurada e mensagem de respawn.
11. Tente atravessar o `AbilityGate` antes do Dash e confirme o bloqueio.
12. Use o menu de contexto `AbilityManager > Debug/Unlock Dash` ou derrote Lucarelli.
13. Confirme que o HUD mostra Dash desbloqueado.
14. Atravesse o gate depois do unlock.
15. Monte Lucarelli na arena de teste, acerte-o com a katana e confirme a recompensa de Dash ao derrotar o chefe.
16. Pressione `ESC`, continue, reinicie a cena e teste o botao de sair do menu de pausa.

## Configuracoes necessarias na Unity
### Cena e build
- Crie e salve uma cena de sala prototipo em `Assets/_Project/Scenes`.
- Adicione a cena ao fluxo de build antes de validar reinicio e quit em build.
- Crie um `GameManager` na cena para checkpoint e respawn.

### Rubens
- GameObject `Rubens_Player` com `Rigidbody2D`, `Collider2D`, `PlayerController2D`, `PlayerHealth`, `PlayerCombat` e `AbilityManager`.
- `Rigidbody2D` dinamico, gravidade ativa e `Rotation Z` congelada.
- Collider corporal solido, nao trigger.
- Filho `GroundCheck` ligado ao `PlayerController2D`.
- Campo `Ground Layer` do controller incluindo `Ground`.
- Filho `AttackPoint` ligado ao `PlayerCombat`.
- Campo `Enemy Layers` do combate incluindo `Enemy`.

### Piso, inimigos, chefe e checkpoint
- Pisos e plataformas com collider solido e Layer `Ground`.
- Inimigo basico com Layer `Enemy`, `Rigidbody2D`, collider e `BasicPatrolEnemy`.
- Lucarelli com Layer `Enemy` para receber a katana, `Rigidbody2D`, collider e `LucarelliBoss`.
- Checkpoint com collider marcado como trigger e ponto de respawn em posicao segura.
- Gate com collider solido e `AbilityGate`; ligar o `AbilityManager` no Inspector abre o gate imediatamente no unlock.

### Camera e UI
- `Main Camera` com `CameraFollow2D` e `Target` apontando para Rubens.
- Canvas com `HUDController` e textos de vida, Dash e mensagem.
- Canvas com `PauseMenuController` ativo fora do `PausePanel`.
- `PausePanel` inicialmente desativado, botoes ligados a `ContinueGame()`, `RestartScene()` e `QuitGame()`.
- `EventSystem` presente para clique dos botoes.

## Resultado da revisao
### Compatibilidade do fluxo
- `PlayerCombat` conversa com inimigo e chefe por `IDamageable`.
- `BasicPatrolEnemy` e `LucarelliBoss` causam dano via `PlayerHealth`.
- `PlayerHealth` aciona `GameManager` ao morrer.
- `AbilityManager` alimenta Dash, gate, HUD e recompensa do chefe.
- Checkpoint, respawn, HUD e pausa operam sem sistema de save.

### Correcoes pequenas aplicadas
- Input do jogador e ataque da katana deixam de processar acoes enquanto `Time.timeScale` esta em `0`.
- Lucarelli nao inicia um novo ataque enquanto o menu de pausa congela o jogo.
- `PlayerController2D` e `CameraFollow2D` foram alinhados aos namespaces `Tester.Player` e `Tester.Core`.
- Layers `Ground` e `Enemy` foram reservadas no `TagManager`.

## Pendencias conhecidas
- Nao ha cena prototipo versionada nesta revisao; a sala ainda precisa ser montada no Editor.
- Nao ha prefab versionado para Rubens, inimigo, Lucarelli, checkpoint, gate, HUD ou menu de pausa.
- Build Settings nao possui cena cadastrada enquanto a sala nao for criada.
- O `GroundCheck`, `AttackPoint`, targets da camera, textos do HUD e painel de pausa dependem de referencias no Inspector.
- `Ground Layer` e `Enemy Layers` precisam ser marcadas nas LayerMasks dos componentes, mesmo com as Layers reservadas.
- Patrulha basica nao detecta beirada ou parede; o layout precisa limitar a patrulha.
- Lucarelli ainda nao tem arena versionada, fase extra, telegraph visual final ou recompensa persistente.
- Respawn e unlock de Dash nao sao persistidos em save.
- HUD, pause menu, efeitos e sprites continuam placeholders.

## Proximas tarefas recomendadas para 0.2
1. Versionar uma cena `BosquePrototypeRoom` pequena com o fluxo completo montado.
2. Criar prefabs simples para Rubens, inimigo basico, checkpoint, Dash gate, Lucarelli e UI de teste.
3. Fazer uma rodada de Play Mode ponta a ponta na cena versionada e ajustar colisores, LayerMasks e pontos de ataque.
4. Adicionar feedback minimo de hit, invulnerabilidade e derrota de chefe com placeholders controlados.
5. Refinar arena de Lucarelli e leitura dos dois ataques sem criar segunda fase.
6. Decidir o menor caminho de persistencia para checkpoint e habilidade em uma etapa futura, separado da validacao atual.
