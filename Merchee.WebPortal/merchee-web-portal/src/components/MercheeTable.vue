<template>
<div class="merchee-table">
    <table>
        <tr class="merchee-table-header">
            <td v-for="column in colums" :key="column.title">
                {{ column.title }}
            </td>
        </tr>
        <tr v-for="item in data" :key="item.id">
            <td v-for="column in colums" :key="column.key">
                {{ item[column.field] }}
            </td>
        </tr>
    </table>
</div>
</template>

<script>
export default {
data() {
    return {
        tableColumns: this.columns,
        tableData: this.data
    };
},
props: ["columns", "data"]
};
</script>