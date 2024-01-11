using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge {

    public class TemplateInfraContext {

        Dictionary<int, RoleTM> roles;

        public TemplateInfraContext() {
            roles = new Dictionary<int, RoleTM>();
        }

        // Role
        public void Role_Add(int id, RoleTM role) {
            roles.Add(id, role);
        }

        public bool Role_TryGet(int id, out RoleTM role) {
            return roles.TryGetValue(id, out role);
        }

    }

}