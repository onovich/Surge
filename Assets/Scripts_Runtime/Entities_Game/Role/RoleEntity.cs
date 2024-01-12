using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge {

    public class RoleEntity : MonoBehaviour {

        // Base Info
        public readonly EntityType entityType = EntityType.Role;
        public int entityID;
        public int typeID;
        public string typeName;
        public AllyStatus allyStatus;
        public RoleType roleType;
        public AIType aiType;
        public float rebornDuration;
        Vector2 faceDir;

        // State
        public bool isDead;

        // Shoot
        [SerializeField] Transform[] muzzles;

        // Physics
        [SerializeField] Rigidbody2D rb;

        // Render
        [SerializeField] SpriteRenderer spr;

        // Target
        public bool hasTarget;
        public EntityType targetEntityType;
        public int targetEntityID;
        public Vector2 targetPos;
        public Vector2 targetDir;

        // Components
        public RoleFSMComponent fsmCom;
        RoleInputComponent inputCom;
        RoleAttrComponent attrCom;
        RoleAnimComponent animCom;
        RoleMoveComponent moveCom;
        RoleDeadDropComponent deadDropCom;
        RoleBuffComponent buffCom;
        RoleSkillComponent skillCom;

        // Ctor
        public void Ctor() {
            gameObject.SetActive(false);
            fsmCom = new RoleFSMComponent();
            inputCom = new RoleInputComponent();
            attrCom = new RoleAttrComponent();
            moveCom = new RoleMoveComponent();
            deadDropCom = new RoleDeadDropComponent();
            buffCom = new RoleBuffComponent();
            skillCom = new RoleSkillComponent();
            animCom = new RoleAnimComponent();
        }

        // Move
        public void Move_Move(float dt) {
            float moveSpeed = Attr_GetMoveSpeed();
            rb.velocity = PFLocomotion.GetVelocity(inputCom.moveAxis, moveSpeed);
        }

        public void Move_Straightly(float dt) {
            float moveSpeed = Attr_GetMoveSpeed();
            rb.velocity = PFLocomotion.GetVelocity(Vector2.down, moveSpeed);
        }

        public void Move_Stop() {
            rb.velocity = Vector2.zero;
        }

        // Pos
        public Vector2 Pos_GetPos() {
            return transform.position;
        }

        public void Pos_SetPos(Vector2 pos) {
            transform.position = pos;
        }

        public Vector2Int Pos_GetPosInt() {
            return new Vector2Int((int)transform.position.x, (int)transform.position.y);
        }

        public bool Pos_IsDifferentFromLast() {
            return Pos_GetPosInt() != Pos_GetLastPosInt();
        }

        public void Pos_RecordLastPosInt() {
            moveCom.lastPosInt = Pos_GetPosInt();
        }

        public Vector2Int Pos_GetLastPosInt() {
            return moveCom.lastPosInt;
        }

        public Vector2 Pos_GetFaceDir() {
            if (faceDir == Vector2.zero) {
                faceDir = Vector2.up;
            }
            return faceDir;
        }


        // Input
        public void Input_SetMoveAxis(Vector2 moveAxis) {
            inputCom.moveAxis = moveAxis;
        }

        public void Input_SetSkillKeyDown(InputKeyEnum skillKeyDown) {
            inputCom.skillKeyDown = skillKeyDown;
        }

        // Attr
        public void Attr_InitAll(float moveSpeed,
                                int hpMax,
                                int atk,
                                int def,
                                float attackRange,
                                float pickRange
                                ) {
            attrCom.moveSpeed = moveSpeed;
            attrCom.hp = hpMax;
            attrCom.hpMax = hpMax;
            attrCom.atk = atk;
            attrCom.def = def;
            attrCom.attackRange = attackRange;
            attrCom.pickRange = pickRange;
        }

        public void Attr_SetMoveSpeed(float moveSpeed) => attrCom.moveSpeed = moveSpeed;
        public float Attr_GetMoveSpeed() {
            float speed = (attrCom.moveSpeed + buffCom.moveSpeedAdd) * buffCom.moveSpeedRate;
            return speed;
        }

        public float Attr_AttackRange() {
            return attrCom.attackRange;
        }

        public int Attr_Hp() => attrCom.hp;
        public void Attr_SetHp(int hp) => attrCom.hp = hp;

        public void Attr_SetAtk(int atk) => attrCom.atk = atk;
        public int Attr_Atk() {
            return attrCom.atk;
        }

        public int Attr_HpMax() => attrCom.hpMax;
        public void Attr_SetHpMax(int hpMax) => attrCom.hpMax = hpMax;

        public void Attr_SetDef(int def) => attrCom.def = def;
        public int Attr_Def() {
            return attrCom.def;
        }

        public void Attr_SetPickRange(float value) => attrCom.pickRange = value;
        public float Attr_PickRange() {
            return attrCom.pickRange;
        }

        // RoleType
        public void RoleType_Set(RoleType roleType) {
            this.roleType = roleType;
        }

        // Skill
        public SkillSubEntity Skill_FindAutoCastNoCD_OnlyOneCycle() {
            return skillCom.FindAutoCastNoCD_OnlyOneCycle();
        }

        public SkillSubEntity Skill_Find(int typeID) {
            return skillCom.Find(typeID);
        }

        public int Skill_TakeAll(out SkillSubEntity[] skills) {
            return skillCom.TakeAll(out skills);
        }

        public void Skill_Foreach(Action<SkillSubEntity> action) {
            skillCom.Foreach(action);
        }

        public SkillSubEntity Skill_GetAttackSkill() {
            return skillCom.GetAttackSkill();
        }


        public SkillSubEntity Skill_GetByKey(InputKeyEnum key) {
            return skillCom.GetByKey(key);
        }

        public void Skill_Add(SkillSubEntity skill) {
            skillCom.Add(skill);
        }

        // Buff
        public void Buff_Add(BuffSubEntity buff) {
            buffCom.TryAddOrUpdate(buff);
        }

        public void Buff_Remove(BuffSubEntity buff) {
            buffCom.Remove(buff);
        }

        public BuffSubEntity Buff_Get(int typeID) {
            return buffCom.Get(typeID);
        }

        public int Buff_TakeAll(out BuffSubEntity[] buffs) {
            return buffCom.TakeAll(out buffs);
        }

        // FSM
        public RoleFSMStatus FSM_GetStatus() {
            return fsmCom.status;
        }

        public void FSM_EnterCasting(SkillSubEntity skill) {
            fsmCom.EnterCasting(skill);
        }

        public void FSM_EnterReborn(float duration) {
            fsmCom.EnterReborn(duration);
        }

        public void FSM_EnterNormal() {
            fsmCom.EnterNormal();
        }

        public void FSM_EnterFakeDead(float duration) {
            fsmCom.EnterFakeDead(duration);
        }

        // Coll
        public void Coll_Deactive() {
            GetComponentInChildren<Collider2D>().enabled = false;
        }

        public void Coll_Active() {
            GetComponentInChildren<Collider2D>().enabled = true;
        }

        public void Coll_SetAsTrigger() {
            GetComponentInChildren<Collider2D>().isTrigger = true;
        }

        public void Coll_SetAsCollision() {
            GetComponentInChildren<Collider2D>().isTrigger = false;
        }

        // Render
        public void Spr_Set(Sprite spr) {
            this.spr.sprite = spr;
        }

        // Pool
        public void Pool_Reuse() {
            gameObject.SetActive(true);
            skillCom.Reuse();
            moveCom.Reuse();
        }

        public void Pool_Release() {
            gameObject.SetActive(false);
            buffCom.Clear();
            skillCom.Clear();
            deadDropCom.Clear();
        }

#if UNITY_EDITOR
        void OnDrawGizmos() {
            Gizmos.color = Color.red;
            if (muzzles == null || muzzles.Length == 0) return;
            foreach (var muzzle in muzzles) {
                if (muzzle == null) continue;
                Gizmos.DrawSphere(muzzle.position, 0.1f);
            }
        }
#endif

    }

}